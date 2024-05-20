using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CafeDuCoin.Data;
using CafeDuCoin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeDuCoin.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GameController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("games")]
        [Authorize]
        public async Task<IActionResult> GetGames()
        {
            var games = await _context.Games.ToListAsync();
            var gameDtos = _mapper.Map<List<GameDto>>(games);
            return Ok(gameDtos);
        }

        [HttpGet("games/{id}/history")]
        [Authorize]
        public async Task<IActionResult> GetGameHistory(int id)
        {
            var rentals = await _context.Rentals
                .Where(r => r.GameId == id)
                .Include(r => r.User)
                .ToListAsync();
            if (rentals == null) return NotFound();

            var rentalDtos = _mapper.Map<List<RentalDto>>(rentals);
            return Ok(rentalDtos);
        }

        [HttpPost("games/{id}/rent")]
        [Authorize]
        public async Task<IActionResult> RentGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null || !game.IsAvailable)
            {
                return BadRequest("Game is not available.");
            }

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var rental = new Rental
            {
                GameId = id,
                UserId = userId,
                RentalDate = DateTime.Now
            };

            game.IsAvailable = false;
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("games/{id}/return")]
        [Authorize]
        public async Task<IActionResult> ReturnGame(int id)
        {
            var rental = await _context.Rentals
                .Where(r => r.GameId == id && r.ReturnDate == null)
                .FirstOrDefaultAsync();

            if (rental == null)
            {
                return BadRequest("No active rental found for this game.");
            }

            rental.ReturnDate = DateTime.Now;
            var game = await _context.Games.FindAsync(id);
            game.IsAvailable = true;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }



}
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
    [Route("api/[controller]")]
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

        // Récupérer la liste de tous les jeux
        [HttpGet("games")]
        [Authorize]
        public async Task<IActionResult> GetGames()
        {
            var games = await _context.Games.ToListAsync();
            var gameDtos = _mapper.Map<List<GameDto>>(games);

            if (gameDtos != null && gameDtos.Any())
            {
                return Ok(gameDtos);
            }
            else
            {
                return NoContent(); 
            }
        }

        // Récupérer l'historique de location d'un jeu spécifique
        [HttpGet("games/{id}/history")]
        [Authorize]
        public async Task<IActionResult> GetGameHistory(int id)
        {
            var rentals = await _context.Rentals
                .Where(r => r.GameId == id)
                .Include(r => r.User)
                .Include(r => r.Game)
                .ToListAsync();

            if (rentals == null || rentals.Count == 0) return NotFound();

            var rentalDtos = _mapper.Map<List<RentalDto>>(rentals);
            return Ok(rentalDtos);
        }

        // Lister tous les jeux loués par l'utilisateur connecté
        [HttpGet("games/rented")]
        [Authorize]
        public async Task<IActionResult> GetGamesRentedByUser()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var rentals = await _context.Rentals
                .Where(r => r.UserId == userId)
                .Include(r => r.Game)
                .ToListAsync();

            if (rentals == null || rentals.Count == 0) return NotFound();

            var rentalDtos = _mapper.Map<List<RentalDto>>(rentals);
            return Ok(rentalDtos);
        }

        // Louer un jeu spécifique
        [HttpPost("games/{id}/rent")]
        [Authorize]
        public async Task<IActionResult> RentGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null || !game.IsAvailable)
            {
                return BadRequest("Game is not available.");
            }

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var rental = new Rental
            {
                GameId = id,
                UserId = userId,
                RentalDate = DateTime.UtcNow
            };

            game.IsAvailable = false;
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // Retourner un jeu loué
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

            rental.ReturnDate =  DateTime.UtcNow;
            var game = await _context.Games.FindAsync(id);
            game.IsAvailable = true;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}

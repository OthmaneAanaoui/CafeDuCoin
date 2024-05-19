using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CafeDuCoin.Models;
using Microsoft.EntityFrameworkCore;
namespace CafeDuCoin.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Rental>()
                .HasOne(l => l.Game)
                .WithMany(j => j.Rentals)
                .HasForeignKey(l => l.GameId);

            modelBuilder.Entity<Rental>()
                .HasOne(l => l.User)
                .WithMany(u => u.Rentals)
                .HasForeignKey(l => l.UserId);
        }
    }
}
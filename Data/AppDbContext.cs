using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
    {
        public DbSet<Card> Card { get; set; }
        public DbSet<List> List { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole { Id = "mediador-id", Name = "Mediador", NormalizedName = "MEDIADOR" },
                    new IdentityRole { Id = "rh-id", Name = "Rh", NormalizedName = "RH" },
                    new IdentityRole { Id = "financeiro-id", Name = "Financeiro", NormalizedName = "FINANCEIRO" },
                    new IdentityRole { Id = "Lideranca-id", Name = "Lideranca", NormalizedName = "LIDERANCA" }
                );
        }
    }
}
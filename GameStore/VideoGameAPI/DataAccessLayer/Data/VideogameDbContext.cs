using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameAPI.Data.Entities;

namespace VideoGameAPI.Data
{
    public class VideogameDbContext : IdentityDbContext
    {
        public DbSet<VideoGameEntity> Videogames { get; set; }

        public VideogameDbContext(DbContextOptions<VideogameDbContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VideoGameEntity>().ToTable("Videogames");
            modelBuilder.Entity<VideoGameEntity>().Property(v => v.Id).ValueGeneratedOnAdd();
        }


        //dotnet tool install --global dotnet-ef
        //dotnet ef migrations add InitialCreate
        //dotnet ef database update

    }
}

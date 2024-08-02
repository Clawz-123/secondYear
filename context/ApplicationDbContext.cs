using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using secondYear.Models;

namespace secondYear.context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Hotel> Hotels{get; set;}

    public DbSet<Review> Reviews{get; set;}

    public DbSet<TravelPackage> TravelPackages{get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>()
            .HasMany(h => h.Reviews)
            .WithOne(r => r.Hotel)
            .HasForeignKey(r => r.HotelId)
            .OnDelete(DeleteBehavior.Cascade);

            // modelBuilder.Entity<Hotel>().HasQueryFilter(h => !h.IsDeleted)      
            
              }    

    }

}
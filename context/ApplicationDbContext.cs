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

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<TravelPackage> TravelPackages { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Review>()
           .HasOne(r => r.User)
           .WithMany(u => u.Reviews)
           .HasForeignKey(r => r.UserId);


            modelBuilder.Entity<Review>()
            .HasOne(r => r.Hotel)
            .WithMany(h => h.Reviews)
            .HasForeignKey(r => r.HotelId);

             modelBuilder.Entity<Booking>()
            .HasOne(b => b.User)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Booking>()
            .HasOne(b => b.Hotel)
            .WithMany(h => h.Bookings)
            .HasForeignKey(b => b.HotelId);



            // modelBuilder.Entity<Hotel>().HasQueryFilter(h => !h.IsDeleted)      


            modelBuilder.Entity<TravelPackage>()
            .HasMany(h => h.Reviews)
            .WithOne(r => r.TravelPackage)
            .HasForeignKey(r => r.TravelId)
            .OnDelete(DeleteBehavior.Cascade);

        }




    }

}
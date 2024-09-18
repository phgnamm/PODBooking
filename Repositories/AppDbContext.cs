using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AppDbContext : IdentityDbContext<Account, Role, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingService> BookingServices { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Pod> Pods { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<RatingComment> RatingComments { get; set; }
        public DbSet<Service> Services { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(x => x.FirstName).HasMaxLength(50);
                entity.Property(x => x.LastName).HasMaxLength(50);
                entity.Property(x => x.PhoneNumber).HasMaxLength(15);
                entity.Property(x => x.VerificationCode).HasMaxLength(6);
            });

            modelBuilder.Entity<Role>(entity => { entity.Property(x => x.Description).HasMaxLength(256); });

            // RatingComment configurations
            modelBuilder.Entity<RatingComment>()
                .HasOne(rc => rc.Account)
                .WithMany()
                .HasForeignKey(rc => rc.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RatingComment>()
                .HasOne(rc => rc.Rating)
                .WithMany(r => r.CommentsList) // Ensure this is set correctly
                .HasForeignKey(rc => rc.RatingId)
                .OnDelete(DeleteBehavior.Restrict);

            // Booking configurations
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Pod)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.PodId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Account)
                .WithMany(a => a.Bookings)
                .HasForeignKey(b => b.AccountId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}


using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Enums;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<CourtAvailability> CourtAvailabilities { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<MonthlyFee> MonthlyFees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Subscription)
                .WithMany();

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Payment>()
            .HasOne(p => p.User)
            .WithMany() 
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.MonthlyFee)
                .WithMany() 
                .HasForeignKey(p => p.MonthlyFeeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

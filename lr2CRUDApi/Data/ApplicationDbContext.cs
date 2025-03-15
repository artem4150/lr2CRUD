using Microsoft.EntityFrameworkCore;
using MyProject.Api.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MyProject.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            // Автоматическое создание БД (для разработки)
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Outfit> Outfits { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Уникальный Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Связь User → Outfit
            modelBuilder.Entity<Outfit>()
                .HasOne(o => o.User)
                .WithMany(u => u.Outfits)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь Outfit → Comment
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Outfit)
                .WithMany(o => o.Comments)
                .HasForeignKey(c => c.OutfitId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

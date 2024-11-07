using Microsoft.EntityFrameworkCore;
using Patrick_T_Assignment_2.Models.Entities;

namespace Patrick_T_Assignment_2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure one-to-many relationship between User and Image
            modelBuilder.Entity<Image>()
                .HasOne(i => i.User)
                .WithMany(u => u.Images)
                .HasForeignKey(i => i.UserId);

            // Configure many-to-many relationship between Image and Tag
            modelBuilder.Entity<Image>()
                .HasMany(i => i.Tags)
                .WithMany(t => t.Images)
                .UsingEntity(j => j.ToTable("ImageTags"));
        }
    }
}
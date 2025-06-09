using AnnouncementBoard.DAL.Configurations;
using AnnouncementBoard.DAL.Entitys;
using Microsoft.EntityFrameworkCore;

namespace AnnouncementBoard.DAL
{
    public class AnnouncementBoardDbContext : DbContext
    {
        public AnnouncementBoardDbContext(DbContextOptions<AnnouncementBoardDbContext> options)
            : base(options)
        {
        }

        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CategoryConfigurations());
            modelBuilder.ApplyConfiguration(new SubCategoryConfigurations());

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.HasOne(s => s.Category)
                    .WithMany(c => c.SubCategories)
                    .HasForeignKey(s => s.CategoryId);
            });

            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.HasOne(a => a.Category)
                    .WithMany(c => c.Announcements)
                    .HasForeignKey(a => a.CategoryId);

                entity.HasOne(a => a.SubCategory)
                    .WithMany(s => s.Announcements)
                    .HasForeignKey(a => a.SubCategoryId);
            });
        }
    }
}

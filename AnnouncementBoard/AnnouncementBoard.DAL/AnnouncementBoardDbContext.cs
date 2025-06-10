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

            modelBuilder.Entity<SubCategory>()
                .HasOne(sc => sc.Category)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(sc => sc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Announcement>()
                .HasOne(a => a.Category)
                .WithMany(c => c.Announcements)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Announcement>()
                .HasOne(a => a.SubCategory)
                .WithMany(sc => sc.Announcements)
                .HasForeignKey(a => a.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

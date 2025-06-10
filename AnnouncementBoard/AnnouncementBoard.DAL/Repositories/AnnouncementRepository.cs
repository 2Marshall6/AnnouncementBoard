using AnnouncementBoard.DAL.Entitys;
using AnnouncementBoard.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnnouncementBoard.DAL.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly AnnouncementBoardDbContext _context;

        public AnnouncementRepository(AnnouncementBoardDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Announcement announcement)
        {
            await _context.Announcements.AddAsync(announcement);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Announcements
                .Where(a => a.Id == id)
                .ExecuteDeleteAsync();

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, string title, string description, bool status, Category category, SubCategory subCategory)
        {
            await _context.Announcements
                .Where(a => a.Id == id)
                .ExecuteUpdateAsync(a => a
                    .SetProperty(a => a.Title, title)
                    .SetProperty(a => a.Description, description)
                    .SetProperty(a => a.Status, status)
                    .SetProperty(a => a.CategoryId, category.Id)
                    .SetProperty(a => a.SubCategoryId, subCategory.Id));
        }

        public async Task<IEnumerable<Announcement>> GetAllAsync()
        {
            var announcements = await _context.Announcements
                .Include(a => a.Category)
                .Include(a => a.SubCategory)
                .AsNoTracking()
                .ToListAsync();

            return announcements;
        }

        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            var category = await _context.Categories
                    .Where(s => s.Name.ToLower() == name.ToLower())
                    .FirstOrDefaultAsync();

            return category;
        }

        public async Task<SubCategory?> GetSubCategoryByNameAsync(string name)
        {
            var subCategory = await _context.SubCategories
                    .Where(s => s.Name.ToLower() == name.ToLower())
                    .FirstOrDefaultAsync();

            return subCategory;
        }
    }
}

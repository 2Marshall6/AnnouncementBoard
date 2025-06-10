using AnnouncementBoard.DAL.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncementBoard.DAL.Interfaces
{
    public interface IAnnouncementRepository
    {
        Task<bool> CreateAsync(Announcement announcement);
        Task<bool> UpdateAsync(int id, string title, string description, bool? status, int? categoryId, int? subCategoryId);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Announcement>> GetListAnnouncementAsync(Category category, SubCategory subCategory);
        Task<Category?> GetCategoryByNameAsync(string name);
        Task<SubCategory?> GetSubCategoryByNameAsync(string name);

    }
}

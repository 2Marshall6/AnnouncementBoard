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
        Task CreateAsync(Announcement announcement);
        Task UpdateAsync(int id, string title, string description, bool status, Category category, SubCategory subCategory);
        Task DeleteAsync(int id);
        Task<IEnumerable<Announcement>> GetAllAsync();
        Task<SubCategory?> GetSubCategoryByNameAsync(string name);
        Task<Category?> GetCategoryByNameAsync(string name);

    }
}

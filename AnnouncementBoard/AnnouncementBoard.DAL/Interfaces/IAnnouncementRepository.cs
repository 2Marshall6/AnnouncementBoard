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
        Task<int> CreateAsync(Announcement announcement);
        Task<IEnumerable<Announcement>> GetAllAsync();
        Task<Announcement?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Announcement announcement);
        Task<bool> DeleteAsync(int id);
    }
}

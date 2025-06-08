using AnnouncementBoard.DAL.Entitys;
using AnnouncementBoard.DAL.Interfaces;

namespace AnnouncementBoard.DAL.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        public Task<int> CreateAsync(Announcement announcement)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Announcement>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Announcement?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Announcement announcement)
        {
            throw new NotImplementedException();
        }
    }
}

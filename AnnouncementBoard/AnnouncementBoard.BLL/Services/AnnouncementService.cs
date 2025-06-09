using AnnouncementBoard.BLL.Interfaces;
using AnnouncementBoard.BLL.Models;
using AnnouncementBoard.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncementBoard.BLL.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _announcementRepository;
        public AnnouncementService(IAnnouncementRepository announcementRepository) 
        {
            _announcementRepository = announcementRepository;
        }

        public Task<AnnouncementDto> CreateAnnouncementAsync(CreateAnnouncementDto createModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAnnouncementAsync(DeleteAnnouncementDto deleteModel)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AnnouncementDto>> GetAnnouncementsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAnnouncementAsync(UpdateAnnouncementDto updateModel)
        {
            throw new NotImplementedException();
        }
    }
}

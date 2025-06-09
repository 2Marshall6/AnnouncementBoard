
using AnnouncementBoard.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncementBoard.BLL.Interfaces
{
    public interface IAnnouncementService
    {
        Task<AnnouncementDto> CreateAnnouncementAsync(CreateAnnouncementDto createModel);
        Task UpdateAnnouncementAsync(UpdateAnnouncementDto updateModel);
        Task<IEnumerable<AnnouncementDto>> GetAnnouncementsAsync();
        Task DeleteAnnouncementAsync(DeleteAnnouncementDto deleteModel);
    }
}

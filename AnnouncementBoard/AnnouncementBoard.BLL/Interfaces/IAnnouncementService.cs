
using AnnouncementBoard.BLL.Models;
using AnnouncementBoard.BLL.Patterns;
using AnnouncementBoard.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncementBoard.BLL.Interfaces
{
    public interface IAnnouncementService
    {
        Task<ServiceResult> CreateAnnouncementAsync(CreateAnnouncementDto createModel);
        Task<ServiceResult> UpdateAnnouncementAsync(UpdateAnnouncementDto updateModel);
        Task<ServiceResult<IEnumerable<AnnouncementDto>>> GetAnnouncementsAsync(FilterAnnouncementDto filterModel);
        Task<ServiceResult> DeleteAnnouncementAsync(DeleteAnnouncementDto deleteModel);
    }
}

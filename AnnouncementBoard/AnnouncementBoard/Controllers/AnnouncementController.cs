using AnnouncementBoard.BLL.Interfaces;
using AnnouncementBoard.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnnouncementBoard.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementsController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnnouncement([FromBody] CreateAnnouncementDto createModel)
        {
            var createdAnnouncement = await _announcementService.CreateAnnouncementAsync(createModel);
            return Ok("Announcement has been created");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAnnouncement([FromBody] UpdateAnnouncementDto updateModel)
        {
            await _announcementService.UpdateAnnouncementAsync(updateModel);
            return Ok("Announcement has been updated");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAnnouncement([FromBody] DeleteAnnouncementDto deleteModel)
        {
            await _announcementService.DeleteAnnouncementAsync(deleteModel);
            return Ok("Announcement has been deleted");
        }

        [HttpGet]
        public async Task<IActionResult> GetAnnouncements()
        {
            var announcements = await _announcementService.GetAnnouncementsAsync();
            return Ok(announcements);
        }
    }
}

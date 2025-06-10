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
        public async Task<IActionResult> CreateAnnouncement([FromQuery] CreateAnnouncementDto createModel)
        {
            var result = await _announcementService.CreateAnnouncementAsync(createModel);
            if (result.IsSuccess)
                return Ok(new { message = result.Message });

            return BadRequest(new { error = result.ErrorMessage });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAnnouncement([FromQuery] UpdateAnnouncementDto updateModel)
        {
            var result = await _announcementService.UpdateAnnouncementAsync(updateModel);
            if (result.IsSuccess)
                return Ok(new { message = result.Message });

            return BadRequest(new { error = result.ErrorMessage });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAnnouncement([FromQuery] DeleteAnnouncementDto deleteModel)
        {
            var result = await _announcementService.DeleteAnnouncementAsync(deleteModel);
            if (result.IsSuccess)
                return Ok(new { message = result.Message });

            return BadRequest(new { error = result.ErrorMessage });
        }

        [HttpGet]
        public async Task<IActionResult> GetAnnouncements([FromQuery] FilterAnnouncementDto filterModel)
        {
            var announcements = await _announcementService.GetAnnouncementsAsync(filterModel);
            if (announcements.IsSuccess)
                return Ok(announcements.Data);

            return StatusCode(500, new { error = announcements.ErrorMessage });
        }
    }
}

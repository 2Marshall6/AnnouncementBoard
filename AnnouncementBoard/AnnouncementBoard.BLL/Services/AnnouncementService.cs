using AnnouncementBoard.BLL.Interfaces;
using AnnouncementBoard.BLL.Models;
using AnnouncementBoard.DAL.Entitys;
using AnnouncementBoard.DAL.Interfaces;

namespace AnnouncementBoard.BLL.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _announcementRepository;
        public AnnouncementService(IAnnouncementRepository announcementRepository) 
        {
            _announcementRepository = announcementRepository;
        }

        public async Task CreateAnnouncementAsync(CreateAnnouncementDto createModel)
        {
            var subCategory = await _announcementRepository.GetSubCategoryByNameAsync(createModel.SubCategory);
            var category = await _announcementRepository.GetCategoryByNameAsync(createModel.Category);


            var announcement = new Announcement
            {
                Id = createModel.Id,
                Title = createModel.Title,
                Description = createModel.Description,
                Status = createModel.Status,
                Category = category,
                SubCategory = subCategory
            };
            await _announcementRepository.CreateAsync(announcement);
        }

        public async Task<IEnumerable<AnnouncementDto>> GetAnnouncementsAsync()
        {
            var announcements = await _announcementRepository.GetAllAsync();

            return announcements.Select(a => new AnnouncementDto(
                a.Id,
                a.Title,
                a.Description,
                a.CreatedDate,
                a.Status,
                a.Category?.Name,
                a.SubCategory?.Name
            ));
        }

        public async Task UpdateAnnouncementAsync(UpdateAnnouncementDto updateModel)
        {
            var category = await _announcementRepository.GetCategoryByNameAsync(updateModel.Category);
            if (category == null)
            {
                throw new NullReferenceException("Such category doesnt exist");
            }

            var subCategory = await _announcementRepository.GetSubCategoryByNameAsync(updateModel.SubCategory);
            if (subCategory == null)
            {
                throw new NullReferenceException("Such subCategory doesnt exist");
            }

            await _announcementRepository.UpdateAsync(updateModel.Id, updateModel.Title, updateModel.Description, updateModel.Status, category, subCategory);
        }

        public async Task DeleteAnnouncementAsync(DeleteAnnouncementDto deleteModel)
        {
            await _announcementRepository.DeleteAsync(deleteModel.Id);
        }
    }
}

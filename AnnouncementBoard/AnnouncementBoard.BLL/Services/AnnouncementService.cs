using AnnouncementBoard.BLL.Interfaces;
using AnnouncementBoard.BLL.Models;
using AnnouncementBoard.BLL.Patterns;
using AnnouncementBoard.DAL.Entitys;
using AnnouncementBoard.DAL.Interfaces;
using AutoMapper;

namespace AnnouncementBoard.BLL.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _announcementRepository;

        public AnnouncementService(
            IAnnouncementRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        public async Task<ServiceResult> CreateAnnouncementAsync(CreateAnnouncementDto createModel)
        {
            try
            {
                var validationResult = await ValidateCategoryAndSubCategoryAsync(
                    createModel.Category,
                    createModel.SubCategory);

                if (!validationResult.IsSuccess)
                    return validationResult;

                var (category, subCategory) = validationResult.Data;

                var announcement = new Announcement
                {
                    Title = createModel.Title,
                    Description = createModel.Description,
                    Status = createModel.Status,
                    CategoryId = category.Id,
                    SubCategoryId = subCategory.Id,
                    CreatedDate = DateTime.UtcNow
                };

                var success = await _announcementRepository.CreateAsync(announcement);

                return CreateOperationResult(success,
                    "Оголошення успішно створено",
                    "Не вдалося створити оголошення");
            }
            catch (Exception ex)
            {
                return ServiceResult.Failure($"Помилка під час створення оголошення: {ex.Message}");
            }
        }

        public async Task<ServiceResult<IEnumerable<AnnouncementDto>>> GetAnnouncementsAsync(FilterAnnouncementDto filterModel)
        {
            try
            {
                var (category, subCategory) = await GetFilterCategoriesAsync(filterModel.Category, filterModel.SubCategory);

                if (filterModel.Category != null && category == null)
                    return ServiceResult<IEnumerable<AnnouncementDto>>.Failure($"Категорія '{filterModel.Category}' не знайдено");

                if (filterModel.SubCategory != null && subCategory == null)
                    return ServiceResult<IEnumerable<AnnouncementDto>>.Failure($"Підкатегорії '{filterModel.SubCategory}' не знайдено");

                var announcements = await _announcementRepository.GetListAnnouncementAsync(category, subCategory);

                var announcementDtos = announcements.Select(a => new AnnouncementDto(
                        a.Id,
                        a.Title,
                        a.Description,
                        a.CreatedDate,
                        a.Status,
                        a.Category?.Name,
                        a.SubCategory?.Name
                    )).ToList();

                return ServiceResult<IEnumerable<AnnouncementDto>>.Success(announcementDtos);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<AnnouncementDto>>.Failure($"Помилка під час отримання оголошень: {ex.Message}");
            }
        }

        public async Task<ServiceResult> UpdateAnnouncementAsync(UpdateAnnouncementDto updateModel)
        {
            try
            {
                if (!await _announcementRepository.ExistsAsync(updateModel.Id))
                    return ServiceResult.Failure($"Оголошення з ID {updateModel.Id} не знайдено");

                var (category, subCategory) = await GetFilterCategoriesAsync(updateModel.Category, updateModel.SubCategory);

                if (updateModel.Category != null && category == null)
                    return ServiceResult<IEnumerable<AnnouncementDto>>.Failure($"Категорія '{updateModel.Category}' не знайдено");

                if (updateModel.SubCategory != null && subCategory == null)
                    return ServiceResult<IEnumerable<AnnouncementDto>>.Failure($"Підкатегорії '{updateModel.SubCategory}' не знайдено");

                var success = await _announcementRepository.UpdateAsync(
                    updateModel.Id,
                    updateModel.Title,
                    updateModel.Description,
                    updateModel.Status,
                    category?.Id,
                    subCategory?.Id);

                return CreateOperationResult(success,
                    "Оголошення успішно оновлено",
                    "Не вдалося оновити оголошення");
            }
            catch (Exception ex)
            {
                return ServiceResult.Failure($"Помилка під час оновлення оголошення: {ex.Message}");
            }
        }

        public async Task<ServiceResult> DeleteAnnouncementAsync(DeleteAnnouncementDto deleteModel)
        {
            try
            {
                if (!await _announcementRepository.ExistsAsync(deleteModel.Id))
                    return ServiceResult.Failure($"Оголошення з ID {deleteModel.Id} не знайдено");

                var success = await _announcementRepository.DeleteAsync(deleteModel.Id);

                return CreateOperationResult(success,
                    "Оголошення успішно видалено",
                    "Не вдалося вилучити оголошення");
            }
            catch (Exception ex)
            {
                return ServiceResult.Failure($"Помилка під час видалення оголошення: {ex.Message}");
            }
        }

        private async Task<ServiceResult<(Category category, SubCategory subCategory)>> ValidateCategoryAndSubCategoryAsync(
            string categoryName,
            string subCategoryName)
        {
            var category = await _announcementRepository.GetCategoryByNameAsync(categoryName);
            if (category == null)
                return ServiceResult<(Category, SubCategory)>.Failure($"Категорія '{categoryName}' не знайдено");

            var subCategory = await _announcementRepository.GetSubCategoryByNameAsync(subCategoryName);
            if (subCategory == null)
                return ServiceResult<(Category, SubCategory)>.Failure($"Підкатегорії '{subCategoryName}' не знайдено");

            if (subCategory.CategoryId != category.Id)
                return ServiceResult<(Category, SubCategory)>.Failure("Підкатегорія не належить до вибраної категорії");

            return ServiceResult<(Category, SubCategory)>.Success((category, subCategory));
        }

        private async Task<(Category? category, SubCategory? subCategory)> GetFilterCategoriesAsync(string categoryName, string subCategoryName)
        {
            Category? category = null;
            SubCategory? subCategory = null;

            if (categoryName != null)
                category = await _announcementRepository.GetCategoryByNameAsync(categoryName);

            if (subCategoryName != null)
                subCategory = await _announcementRepository.GetSubCategoryByNameAsync(subCategoryName);

            return (category, subCategory);
        }

        private static ServiceResult CreateOperationResult(bool success, string successMessage, string failureMessage)
        {
            return success ? ServiceResult.Success(successMessage) : ServiceResult.Failure(failureMessage);
        }
    }
}
using AnnouncementBoard.DAL.Entitys;
using AnnouncementBoard.DAL.Interfaces;
using AnnouncementBoard.DAL.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;

namespace AnnouncementBoard.DAL.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly AnnouncementBoardDbContext _context;

        public AnnouncementRepository(AnnouncementBoardDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Announcement announcement)
        {
            ValidateAnnouncementNotNull(announcement);

            try
            {
                var resultParameter = new SqlParameter("@Result", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                var parameters = new[]
                {
                    new SqlParameter("@Title", announcement.Title ?? (object)DBNull.Value),
                    new SqlParameter("@Description", announcement.Description ?? (object)DBNull.Value),
                    new SqlParameter("@Status", announcement.Status),
                    new SqlParameter("@CategoryId", announcement.CategoryId),
                    new SqlParameter("@SubCategoryId", announcement.SubCategoryId),
                    new SqlParameter("@CreatedDate", announcement.CreatedDate),
                    resultParameter
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_CreateAnnouncement @Title, @Description, @Status, @CategoryId, @SubCategoryId, @CreatedDate, @Result OUTPUT",
                    parameters);

                var result = (int)resultParameter.Value;

                if (result > 0)
                {
                    announcement.Id = result;
                    return true;
                }
                else if (result == -1)
                {
                    throw new DataAccessException("Категория не найдена");
                }
                else if (result == -2)
                {
                    throw new DataAccessException("Подкатегория не найдена");
                }

                return false;
            }
            catch (SqlException ex)
            {
                throw new DataAccessException("Помилка під час створення оголошення в базі даних", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var rowsAffectedParameter = new SqlParameter("@RowsAffected", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                var parameters = new[]
                {
                    new SqlParameter("@Id", id),
                    rowsAffectedParameter
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_DeleteAnnouncement @Id, @RowsAffected OUTPUT",
                    parameters);

                var rowsAffected = (int)rowsAffectedParameter.Value;
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw new DataAccessException("Помилка при видаленні оголошення з бази даних", ex);
            }
        }

        public async Task<bool> UpdateAsync(int id, string title, string description, bool? status, int? categoryId, int? subCategoryId)
        {
            try
            {
                var rowsAffectedParameter = new SqlParameter("@RowsAffected", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                var parameters = new[]
                {
                    new SqlParameter("@Id", id),
                    new SqlParameter("@Title", title ?? (object)DBNull.Value),
                    new SqlParameter("@Description", description ?? (object)DBNull.Value),
                    new SqlParameter("@Status", status ?? (object)DBNull.Value),
                    new SqlParameter("@CategoryId", categoryId ?? (object)DBNull.Value),
                    new SqlParameter("@SubCategoryId", subCategoryId ?? (object)DBNull.Value),
                    new SqlParameter("@ModifiedDate", DateTime.UtcNow),
                    rowsAffectedParameter
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_UpdateAnnouncement @Id, @Title, @Description, @Status, @CategoryId, @SubCategoryId, @ModifiedDate, @RowsAffected OUTPUT",
                    parameters);

                var rowsAffected = (int)rowsAffectedParameter.Value;

                if (rowsAffected == -1)
                {
                    throw new DataAccessException("Категория не найдена");
                }
                else if (rowsAffected == -2)
                {
                    throw new DataAccessException("Подкатегория не найдена");
                }

                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw new DataAccessException("Помилка під час оновлення оголошення бази даних", ex);
            }
        }

        public async Task<IEnumerable<Announcement>> GetListAnnouncementAsync(Category? category, SubCategory? subCategory)
        {
            try
            {
                var categoryIdParam = category != null
                    ? new SqlParameter("@CategoryId", category.Id)
                    : new SqlParameter("@CategoryId", DBNull.Value);

                var subCategoryIdParam = subCategory != null
                    ? new SqlParameter("@SubCategoryId", subCategory.Id)
                    : new SqlParameter("@SubCategoryId", DBNull.Value);

                var announcements = await _context.Announcements
                    .FromSqlRaw("EXEC sp_GetAnnouncementsFiltered @CategoryId, @SubCategoryId", categoryIdParam, subCategoryIdParam)
                    .AsNoTracking()
                    .ToListAsync();

                foreach (var a in announcements)
                {
                    a.Category = await _context.Categories.FindAsync(a.CategoryId);
                    a.SubCategory = await _context.SubCategories.FindAsync(a.SubCategoryId);
                }

                return announcements;
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Помилка під час отримання оголошень з бази даних", ex);
            }
        }

        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            try
            {
                var parameter = new SqlParameter("@Name", name);

                var categories = await _context.Categories
                    .FromSqlRaw("EXEC sp_GetCategoryByName @Name", parameter)
                    .AsNoTracking()
                    .ToListAsync();

                return categories.FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw new DataAccessException($"Помилка отримання категорії '{name}'", ex);
            }
        }

        public async Task<SubCategory?> GetSubCategoryByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            try
            {
                var parameter = new SqlParameter("@Name", name);

                var subCategories = await _context.SubCategories
                    .FromSqlRaw("EXEC sp_GetSubCategoryByName @Name", parameter)
                    .AsNoTracking()
                    .ToListAsync();

                return subCategories.FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw new DataAccessException($"Помилка отримання підкатегорії '{name}'", ex);
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            if (id <= 0)
                return false;

            try
            {
                var existsParameter = new SqlParameter("@Exists", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };

                var parameters = new[]
                {
                    new SqlParameter("@Id", id),
                    existsParameter
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_CheckAnnouncementExists @Id, @Exists OUTPUT",
                    parameters);

                return (bool)existsParameter.Value;
            }
            catch (SqlException ex)
            {
                throw new DataAccessException($"Помилка при перевірці наявності ID оголошення {id}", ex);
            }
        }

        private static void ValidateAnnouncementNotNull(Announcement announcement)
        {
            if (announcement == null)
                throw new ArgumentNullException(nameof(announcement));
        }
    }
}
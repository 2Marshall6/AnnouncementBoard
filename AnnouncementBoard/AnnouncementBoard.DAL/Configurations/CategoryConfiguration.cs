using AnnouncementBoard.DAL.Entitys;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AnnouncementBoard.DAL.Configurations
{
    class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasData(
                new Category { Id = 1, Name = "Побутова техніка" },
                new Category { Id = 2, Name = "Комп'ютерна техніка" },
                new Category { Id = 3, Name = "Смартфони" },
                new Category { Id = 4, Name = "Інше" }
            );
        }
    }
}

using AnnouncementBoard.DAL.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnnouncementBoard.DAL.Configurations
{
    class SubCategoryConfigurations : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasKey(sc => sc.Id);

            builder.HasData(
                new SubCategory { Id = 1, Name = "Холодильники", CategoryId = 1 },
                new SubCategory { Id = 2, Name = "Пральні машини", CategoryId = 1 },
                new SubCategory { Id = 3, Name = "Бойлери", CategoryId = 1 },
                new SubCategory { Id = 4, Name = "Печі", CategoryId = 1 },
                new SubCategory { Id = 5, Name = "Витяжки", CategoryId = 1 },
                new SubCategory { Id = 6, Name = "Мікрохвильові печі", CategoryId = 1 },

                new SubCategory { Id = 7, Name = "ПК", CategoryId = 2 },
                new SubCategory { Id = 8, Name = "Ноутбуки", CategoryId = 2 },
                new SubCategory { Id = 9, Name = "Монітори", CategoryId = 2 },
                new SubCategory { Id = 10, Name = "Принтери", CategoryId = 2 },
                new SubCategory { Id = 11, Name = "Сканери", CategoryId = 2 },

                new SubCategory { Id = 12, Name = "Android смартфони", CategoryId = 3 },
                new SubCategory { Id = 13, Name = "iOS/Apple смартфони", CategoryId = 3 },

                new SubCategory { Id = 14, Name = "Одяг", CategoryId = 4 },
                new SubCategory { Id = 15, Name = "Взуття", CategoryId = 4 },
                new SubCategory { Id = 16, Name = "Аксесуари", CategoryId = 4 },
                new SubCategory { Id = 17, Name = "Спортивне обладнання", CategoryId = 4 },
                new SubCategory { Id = 18, Name = "Іграшки", CategoryId = 4 }
            );
        }
    }
}
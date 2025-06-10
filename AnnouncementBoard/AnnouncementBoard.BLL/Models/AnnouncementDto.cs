using AnnouncementBoard.DAL.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncementBoard.BLL.Models
{
    public record AnnouncementDto(int Id, string Title, string Description, DateTime CreatedDate, bool Status, string Category, string SubCategory);
}

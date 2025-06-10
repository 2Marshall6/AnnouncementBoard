using AnnouncementBoard.DAL.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncementBoard.BLL.Models
{
    public record FilterAnnouncementDto(string? Category, string? SubCategory);
}

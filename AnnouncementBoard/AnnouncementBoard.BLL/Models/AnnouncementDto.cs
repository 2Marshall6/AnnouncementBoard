using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncementBoard.BLL.Models
{
    public record AnnouncementDto(int id, string title, string description, DateTime createdDate, bool status, string category, string subCategory);
}

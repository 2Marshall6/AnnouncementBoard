using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncementBoard.BLL.Models
{
    public record UpdateAnnouncementDto(int id, string title, string description, string status, string category, string subCategory);
}

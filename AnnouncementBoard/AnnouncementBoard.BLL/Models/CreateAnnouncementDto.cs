using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncementBoard.BLL.Models
{
    public record CreateAnnouncementDto(string Title, string Description, bool Status, string Category, string SubCategory);
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace KanbanBoard.Models
{
    public class KanbanTask
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [ForeignKey("ResponsibleUser")]
        public int ResponsibleUserRefId { get; set; }
        public IdentityUser ResponsibleUser { get; set; }

        [ForeignKey("Owner")]
        public int OwnerRefId { get; set; }
        public IdentityUser Owner { get; set; }
    }
}

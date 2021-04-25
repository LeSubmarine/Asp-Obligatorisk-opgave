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
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Time { get; set; }

        [Required]
        public string Movement { get; set; }

        [ForeignKey("ResponsibleUser")]
        public string ResponsibleUserRefId { get; set; }
        public IdentityUser ResponsibleUser { get; set; }

        [ForeignKey("Owner")]
        public string OwnerRefId { get; set; }
        public IdentityUser Owner { get; set; }
    }
}

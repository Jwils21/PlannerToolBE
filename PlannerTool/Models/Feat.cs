using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlannerTool.Models
{
    public class Feat
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        [Required]
        public bool Active { get; set; } = true;

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public Feat() { }

    }
}
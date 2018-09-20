using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlannerTool.Models
{
    public class Goal
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [StringLength(30)]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [Required]
        [StringLength(30)]
        public DateTime EndDate { get; set; }
        [Required]
        [StringLength(30)]
        public string Email { get; set; }
        [Required]
        [StringLength(12)]
        public string Phone { get; set; }
        [Required]
        public bool IsReviewer { get; set; } = false;
        [Required]
        public bool IsAdmin { get; set; } = false;
        [Required]
        public bool Active { get; set; } = true;

    }
}
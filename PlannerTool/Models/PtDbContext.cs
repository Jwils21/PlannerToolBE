using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PlannerTool.Models
{
    public class PtDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public PtDbContext() : base() { }
    }
}
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
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Objective> Objectives { get; set; }
        public DbSet<Feat> Feats { get; set; }


        public PtDbContext() : base() { }
    }
}
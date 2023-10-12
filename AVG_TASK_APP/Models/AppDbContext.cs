using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVG_TASK_APP.Migrations.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using AVG_TASK_APP.Models;

namespace AVG_TASK_APP.Migration
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new NotifyConfiguration());
        }

        public DbSet <UserModel>Users { get; set; }
        public DbSet<Notify> NotifyConfigurations { get; set; }
    }
}

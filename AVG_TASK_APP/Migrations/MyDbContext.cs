using AVG_TASK_APP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVG_TASK_APP.Migrations.Config;
namespace AVG_TASK_APP.Migrations
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new NotifyConfiguration());
        }

        // DbSet properties and other configurations...
        public DbSet<UserModel>  Users { get; set; }
        public DbSet<NotifyModel> Notifies { get; set; }
    }


}

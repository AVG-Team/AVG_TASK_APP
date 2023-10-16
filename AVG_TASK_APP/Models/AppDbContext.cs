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
            modelBuilder.ApplyConfiguration(new WorkspaceConfiguration());
            modelBuilder.ApplyConfiguration(new UserWorkspaceConfiguration());
            modelBuilder.ApplyConfiguration(new TableConfiguration());
            modelBuilder.ApplyConfiguration(new UserTableConfiguration());
            modelBuilder.ApplyConfiguration(new TaskConfiguration());
            modelBuilder.ApplyConfiguration(new UserTaskConfiguration());
            modelBuilder.ApplyConfiguration(new CardConfiguration());
            modelBuilder.ApplyConfiguration(new MiniTaskConfiguration());
        }

        public DbSet <UserModel>Users { get; set; }
        public DbSet<Notify> Notifies { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<UserWorkspace> UserWorkspaces { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<UserTable> UserTables { get; set; }
        public DbSet<System.Threading.Tasks.Task> Tasks { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<MiniTask> MiniTasks { get; set; }
        public DbSet<Card> Cards { get; set; }

    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Models.Configuaration
{
    public class UserWorkspaceConfiguration : IEntityTypeConfiguration<UserWorkspace>
    {
        public void Configure(EntityTypeBuilder<UserWorkspace> builder)
        {
            builder.ToTable("User Workspaces");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Role).IsRequired();
            builder.HasOne(x => x.User).WithMany(x => x.UserWorkspaces).HasForeignKey(x => x.Id_User);
            builder.HasOne(x => x.Workspace).WithMany(x => x.UserWorkspaces).HasForeignKey(x => x.Id_Workspace);
        }
    }
}

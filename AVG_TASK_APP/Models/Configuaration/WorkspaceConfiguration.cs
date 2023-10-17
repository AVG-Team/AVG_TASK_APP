using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Models.Configuaration
{
    public class WorkspaceConfiguration : IEntityTypeConfiguration<Workspace>
    {
        public void Configure(EntityTypeBuilder<Workspace> builder)
        {
            builder.ToTable("Workspaces");
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.Name).HasMaxLength(200).IsRequired();
            builder.Property(t => t.Date).IsRequired();
            builder.Property(t => t.Visible).HasDefaultValue(false); ;
            builder.Property(t => t.Code).HasMaxLength(2000).IsRequired();
            builder.Property(t => t.Created_At);
            builder.Property(t => t.Deleted_At).HasDefaultValue(null);
        }
    }
}

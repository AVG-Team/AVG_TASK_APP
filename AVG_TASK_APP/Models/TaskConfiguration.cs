using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Models
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.ToTable("Tasks");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Activity).HasDefaultValue(false);
            builder.Property(x => x.Label).IsRequired();
            builder.Property(x => x.Estimate).IsRequired();
            builder.Property(x => x.Deadline).IsRequired();
            builder.Property(x => x.Deleted_At).HasDefaultValue(null);
            builder.Property(x => x.Created_At).IsRequired();
            builder.HasOne(x => x.Table).WithMany(x => x.tasks).HasForeignKey(x => x.Id_Table);
            builder.HasOne(x => x.Card).WithMany(x => x.tasks).HasForeignKey(x => x.Id_Card);
        }
    }
}

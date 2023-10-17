using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Models.Configuaration
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.ToTable("Task");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).UseMySqlIdentityColumn();
            builder.Property(t => t.Name).HasMaxLength(200).IsRequired();
            builder.Property(t => t.Description).HasMaxLength(200).IsRequired();
            builder.Property(t => t.Activity).HasDefaultValue(false);
            builder.Property(t => t.Estimate).HasMaxLength(200).IsRequired();
            builder.Property(t => t.Deadline).IsRequired();
            builder.Property(t => t.Deleted_At).HasDefaultValue(null);
            builder.Property(t => t.Created_At);

            builder.HasOne(x => x.Table).WithMany(x => x.tasks).HasForeignKey(x => x.Id_Table);
            builder.HasOne(x => x.Card).WithMany(x => x.tasks).HasForeignKey(x => x.Id_Card);
        }
    }
}

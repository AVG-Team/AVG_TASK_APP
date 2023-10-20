using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Models.Configuaration
{
    public class MiniTaskConfiguration : IEntityTypeConfiguration<MiniTask>
    {
        public void Configure(EntityTypeBuilder<MiniTask> builder)
        {
            builder.ToTable("Mini Tasks");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Status).HasDefaultValue(false);
            builder.HasOne(x => x.Task).WithMany(x => x.miniTasks).HasForeignKey(x => x.Id_Task);
        }
    }
}

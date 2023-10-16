using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Models
{
    public class TableConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.ToTable("Tables");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Pin).HasDefaultValue(false);
            builder.Property(x => x.Visible).HasDefaultValue(false);
            builder.Property(x => x.Code).HasMaxLength(2000).IsRequired();
            builder.Property(x => x.Deleted_At).HasDefaultValue(null);
            builder.HasKey(x => x.Created_At);
            builder.HasOne(x => x.Workspace).WithMany(x => x.tables).HasForeignKey(x => x.Id_Workspace);
        }
    }
}

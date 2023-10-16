using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Models
{
    public class UserTableConfiguration : IEntityTypeConfiguration<UserTable>
    {
        public void Configure(EntityTypeBuilder<UserTable> builder)
        {
            builder.ToTable("User Tables");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();
            builder.Property(x => x.Role).HasMaxLength(200).IsRequired();
            builder.HasOne(x => x.User).WithMany(x => x.userTables).HasForeignKey(x => x.Id_User);
            builder.HasOne(x => x.Table).WithMany(x => x.userTables).HasForeignKey(x => x.Id_Table);
        }
    }
}

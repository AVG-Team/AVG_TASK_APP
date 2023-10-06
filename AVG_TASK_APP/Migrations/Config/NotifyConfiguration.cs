using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AVG_TASK_APP.Models;
using K4os.Hash.xxHash;

namespace AVG_TASK_APP.Migrations.Config
{
    public class NotifyConfiguration : IEntityTypeConfiguration<NotifyModel>
    {
        public void Configure(EntityTypeBuilder<NotifyModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany(x => x.notifies).HasForeignKey(x => x.Id_User);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.Pin).HasDefaultValue(false); // Default value for 'Pin'
        }
    }
}

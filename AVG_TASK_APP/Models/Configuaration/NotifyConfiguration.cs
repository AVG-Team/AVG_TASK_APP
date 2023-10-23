using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AVG_TASK_APP.Models;
using K4os.Hash.xxHash;

namespace AVG_TASK_APP.Models.Configuaration
{
    public class NotifyConfiguration : IEntityTypeConfiguration<Notify>
    {
        public void Configure(EntityTypeBuilder<Notify> builder)
        {
            builder.ToTable("Notifies");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.Pin).HasDefaultValue(false);
            builder.Property(x => x.Created_At);
            builder.HasOne(x => x.User).WithMany(x => x.notifies).HasForeignKey(x => x.Id_User);
        }
    }
}
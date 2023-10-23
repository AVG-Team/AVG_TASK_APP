using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Models.Configuaration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Message).HasMaxLength(2000).IsRequired();
            builder.Property(x => x.Created_At);
            builder.HasOne(x => x.Task).WithMany(x => x.Comments).HasForeignKey(x => x.Id_Task);
            builder.HasOne(x => x.User).WithMany(x => x.Comments).HasForeignKey(x => x.Id_User);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AVG_TASK_APP.Models;

namespace AVG_TASK_APP.Models.Configuaration
{
    public class UserConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.Name).HasMaxLength(200).IsRequired();
            builder.Property(t => t.Email).HasMaxLength(200).IsRequired();
            builder.Property(t => t.Password).HasMaxLength(200).IsRequired();
            builder.Property(t => t.PhoneNumber).HasMaxLength(200).IsRequired();
            builder.Property(t => t.Salt).IsRequired();
            builder.Property(t => t.Level).IsRequired();
        }
    }
}
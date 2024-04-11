using ASPNetCore_WebAPI_BookStore_Website.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCore_WebAPI_BookStore_Website.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.HasKey(u => u.UserId);

            builder.Property(u => u.UserId)
                .HasColumnName("UserId")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(u => u.UserName)
                .HasColumnType("Varchar(50)")
                .IsRequired();

            builder.Property(u => u.Password)
                .HasColumnType("Varchar(50)")
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnType("Varchar(50)")
                .IsRequired();

            builder.Property(u => u.Role)
                .HasColumnType("Varchar(30)")
                .IsRequired();

            builder.HasIndex(u => u.UserName)
                .IsUnique();

            builder.HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}

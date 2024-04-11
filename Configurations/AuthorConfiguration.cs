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
    public class AuthorConfiguration : IEntityTypeConfiguration<Authors>
    {
        public void Configure(EntityTypeBuilder<Authors> builder)
        {
            builder.HasKey(a => a.AuthorId);

            builder.Property(a => a.AuthorId)
                .HasColumnName("AuthorId")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(a => a.FirstName)
                .HasColumnType("Varchar(100)")
                .IsRequired();

            builder.Property(a => a.LastName)
                .HasColumnType("Varchar(100)")
                .IsRequired();
        }
    }
}

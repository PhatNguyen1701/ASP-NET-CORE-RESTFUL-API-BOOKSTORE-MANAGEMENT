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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customers>
    {
        public void Configure(EntityTypeBuilder<Customers> builder)
        {
            builder.HasKey(b => b.CustomerId);

            builder.Property(b => b.CustomerId)
                .HasColumnName("CustomerId")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(b => b.FirstName)
                .HasColumnType("Varchar(100)")
                .IsRequired();

            builder.Property(b => b.LastName)
                .HasColumnType("Varchar(100)")
                .IsRequired();

            builder.Property(b => b.StreetName)
                .HasColumnType("Varchar(100)")
                .IsRequired();

            builder.Property(b => b.StreetNumber)
                .IsRequired();

            builder.Property(b => b.Country)
                .HasColumnType("Varchar(50)")
                .IsRequired();

            builder.Property(b => b.PhoneNumber)
                .HasColumnType("Varchar(30)")
                .IsRequired();
        }
    }
}

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
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.BookId);

            builder.Property(b => b.BookId)
                .HasColumnName("BookId")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(b => b.Title)
                .HasColumnType("Varchar(100)")
                .IsRequired();

            builder.Property(b => b.Type)
                .HasColumnType("Varchar(50)")
                .IsRequired();

            builder.Property(b => b.PublicationYear)
                .IsRequired();

            builder.Property(b => b.Price)
                .IsRequired();
        }
    }
}

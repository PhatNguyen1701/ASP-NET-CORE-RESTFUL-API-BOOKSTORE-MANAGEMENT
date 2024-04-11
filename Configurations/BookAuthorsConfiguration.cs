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
    public class BookAuthorsConfiguration : IEntityTypeConfiguration<BookAuthors>
    {
        public void Configure(EntityTypeBuilder<BookAuthors> builder)
        {
            builder.HasKey(ba => ba.BookAuthorsId);

            builder.Property(ba => ba.BookAuthorsId)
                .HasColumnName("BookAuthorId")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(ba => ba.BookId)
                .IsRequired();

            builder.Property(ba => ba.AuthorId)
                .IsRequired();

            builder.HasOne(b => b.Book)
                .WithMany(ba => ba.BookAuthors)
                .HasForeignKey(ba => ba.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Authors)
                .WithMany(ba => ba.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

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
    public class PublisherConfiguration : IEntityTypeConfiguration<Publishers>
    {
        public void Configure(EntityTypeBuilder<Publishers> builder)
        {
            builder.HasKey(p => p.PublisherId);

            builder.Property(p => p.PublisherId)
                .HasColumnName("PublisherId")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(p => p.Country)
                .HasColumnType("Varchar(50)")
                .IsRequired();

            builder.HasOne(b => b.Book)
                .WithMany(p => p.Publishers)
                .HasForeignKey(p => p.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

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
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.HasKey(i => i.InventoryId);

            builder.Property(i => i.InventoryId)
                .HasColumnName("InventoryId")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.HasOne(b => b.Book)
                .WithMany(i => i.Inventories)
                .HasForeignKey(i => i.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

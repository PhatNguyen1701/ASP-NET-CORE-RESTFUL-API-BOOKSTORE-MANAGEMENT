using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCore_WebAPI_BookStore_Website.Data
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public int PublicationYear { get; set; }
        public double Price { get; set; }
        public string Condition { get; set; }

        public ICollection<BookAuthors> BookAuthors { get; set; }
        public ICollection<Inventory> Inventories  { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<Publishers> Publishers { get; set; }
    }
}

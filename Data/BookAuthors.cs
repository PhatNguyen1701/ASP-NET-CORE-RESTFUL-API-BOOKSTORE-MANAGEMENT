using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCore_WebAPI_BookStore_Website.Data
{
    public class BookAuthors
    {
        public int BookAuthorsId { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }


        public Book Book { get; set; }
        public Authors Authors { get; set; }
    }
}

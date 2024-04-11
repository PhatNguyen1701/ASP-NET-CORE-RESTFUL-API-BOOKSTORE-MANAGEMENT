using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCore_WebAPI_BookStore_Website.ViewModels
{
    public class BookAuthorVM
    {
        public int BookAuthorsId { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }
    }
}

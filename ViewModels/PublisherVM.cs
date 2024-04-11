using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCore_WebAPI_BookStore_Website.ViewModels
{
    public class PublisherVM
    {
        public int PublisherId { get; set; }
        public string Country { get; set; }
        public int? BookId { get; set; }
    }
}

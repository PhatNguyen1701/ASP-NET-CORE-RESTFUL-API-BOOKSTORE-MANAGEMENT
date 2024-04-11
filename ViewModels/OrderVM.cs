using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCore_WebAPI_BookStore_Website.ViewModels
{
    public class OrderVM
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public int SubTotal { get; set; }
        public int Shipping { get; set; }
        public int Total { get; set; }
    }
}

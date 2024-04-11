using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCore_WebAPI_BookStore_Website.Data
{
    public class Customers
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}

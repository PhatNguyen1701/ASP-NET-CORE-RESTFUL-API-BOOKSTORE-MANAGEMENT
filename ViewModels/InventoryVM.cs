using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCore_WebAPI_BookStore_Website.ViewModels
{
    public class InventoryVM
    {
        public int InventoryId { get; set; }
        public int? BookId { get; set; }
        public int StockLevelUsed { get; set; }
        public int StockLevelNew { get; set; }
    }
}

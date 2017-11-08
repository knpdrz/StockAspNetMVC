using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbeddedStock.Models.StockViewModels
{
    public class CategoryData
    {
        public long CategoryID { get; set; }
        public String Name { get; set; }
        public bool Assigned { get; set; }
    }
}

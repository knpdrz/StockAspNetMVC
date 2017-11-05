using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbeddedStock.Models
{
    //class representing many-to-many relationship
    //between a Category and ComponentType
    public class ComponentTypeCategory
    {
        public int CategoryID { get; set; }
        public long ComponentTypeID { get; set; }

        //navigation properties
        public ComponentType ComponentType { get; set; }
        public Category Category { get; set; }
    }
}

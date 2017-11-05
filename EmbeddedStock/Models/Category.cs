using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbeddedStock.Models
{
    public class Category
    {
        public Category()
        {
            //ComponentTypes = new List<ComponentType>();

        }
        public int CategoryID { get; set; }
        public string Name { get; set; }

        public ICollection<ComponentTypeCategory> ComponentTypeCategories { get; set; }

        /*public ICollection<ComponentType> ComponentTypes
        {
            get; protected set;
        }*/
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbeddedStock.Models.StockViewModels
{
    public class ComponentTypeIndexData
    {
        public IEnumerable<ComponentType> ComponentTypes { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public ComponentType ComponentType { get; set; }
    }
}

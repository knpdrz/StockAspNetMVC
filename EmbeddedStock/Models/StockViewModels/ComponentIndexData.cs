using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbeddedStock.Models.StockViewModels
{
    public class ComponentIndexData
    {
        public IEnumerable<Component> Components { get; set; }
        public IEnumerable<ComponentType> ComponentTypes { get; set; }
        public Component Component { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbeddedStock.Models
{
    public enum ComponentTypeStatus { Available, ReservedAdmin }

    public class ComponentType
    {
        
        public long ComponentTypeID { get; set; }
        public string ComponentTypeName { get; set; }
        public string ComponentInfo { get; set; }
        public string Location { get; set; }
        public ComponentTypeStatus Status { get; set; }
        public string Datasheet { get; set; }
        public string ImageUrl { get; set; }
        public string Manufacturer { get; set; }
        public string WikiLink { get; set; }
        public string AdminComment { get; set; }
        public virtual ESImage Image { get; set; }
        public ICollection<Component> Components { get; protected set; }
        //public ICollection<Category> Categories { get; protected set; }

        //navigation property
        public ICollection<ComponentTypeCategory> ComponentTypeCategories { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmbeddedStock.Models
{
    public enum ComponentTypeStatus { Available, ReservedAdmin }

    public class ComponentType
    {
        
        public long ComponentTypeID { get; set; }
        [Required]
        [Display(Name="Component type name")]
        public string ComponentTypeName { get; set; }
        [Display(Name = "Component info")]
        public string ComponentInfo { get; set; }
        public string Location { get; set; }
        public ComponentTypeStatus Status { get; set; }
        public string Datasheet { get; set; }
        [Display(Name = "Image url")]
        public string ImageUrl { get; set; }
        public string Manufacturer { get; set; }
        [Display(Name = "Wiki link")]
        public string WikiLink { get; set; }
        [Display(Name = "Admin comment")]
        public string AdminComment { get; set; }
        public virtual ESImage Image { get; set; }
        public ICollection<Component> Components { get; protected set; }

        //navigation property
        public ICollection<ComponentTypeCategory> ComponentTypeCategories { get; set; }

    }
}

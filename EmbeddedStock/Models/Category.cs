using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmbeddedStock.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<ComponentTypeCategory> ComponentTypeCategories { get; set; }

        /*public ICollection<ComponentType> ComponentTypes
        {
            get; protected set;
        }*/
    }
}

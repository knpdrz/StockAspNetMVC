using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmbeddedStock.Models
{
    public enum ComponentStatus
    {
        Available,
        ReservedLoaner,
        ReservedAdmin,
        Loaned, Defect,
        Trashed,
        Lost,
        NeverReturned
    }

    public class Component
    {
        public long ComponentID { get; set; }//primary key
        public long ComponentTypeID { get; set; }//foreign key
        [Display(Name="Component nr")]
        public int ComponentNumber { get; set; }
        [Display(Name = "Serial nr")]
        public string SerialNo { get; set; }
        public ComponentStatus Status { get; set; }
        [Display(Name = "Admin comment")]
        public string AdminComment { get; set; }
        [Display(Name = "User comment")]
        public string UserComment { get; set; }
        [Display(Name = "Cur loan info id")]
        public long? CurrentLoanInformationId { get; set; }
//navigation property
        [Display(Name = "Component type")]        
        public ComponentType ComponentType { get; set; }
       
    }
}
//category many ---- many component type
//component type 1 ----- many component
//student 1--- enrollment
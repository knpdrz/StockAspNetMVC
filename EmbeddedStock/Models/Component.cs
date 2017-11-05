using System;
using System.Collections.Generic;
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
        public int ComponentNumber { get; set; }
        public string SerialNo { get; set; }
        public ComponentStatus Status { get; set; }
        public string AdminComment { get; set; }
        public string UserComment { get; set; }
        public long? CurrentLoanInformationId { get; set; }

        //navigation property
        public ComponentType ComponentType { get; set; }
       
    }
}
//category many ---- many component type
//component type 1 ----- many component
//student 1--- enrollment
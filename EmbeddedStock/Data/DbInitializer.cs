using EmbeddedStock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbeddedStock.Data
{
    public static class DbInitializer
    {
        public static void Initialize(StockContext context)
        {
            context.Database.EnsureCreated();

            //check if there db was already created
            if (context.Components.Any())
            {
                return;
            }
            
            var categories = new Category[]
            {
                new Category{Name="IoT"},
                new Category{Name="Tablet"},
                new Category{Name="PC"},
                new Category{Name="Smartphone"}

            };

            foreach(Category c in categories)
            {
                context.Categories.Add(c);
            }

            context.SaveChanges();
           
          
            var componentTypes = new ComponentType[]
            {
                new ComponentType{ComponentName="Photon", ComponentInfo="", AdminComment="", Datasheet="", ImageUrl="",Manufacturer="",WikiLink="",Location="", Image=new ESImage(), Status=ComponentTypeStatus.Available},
                new ComponentType{ComponentName="Electron", ComponentInfo="", AdminComment="", Datasheet="", ImageUrl="",Manufacturer="",WikiLink="",Location="", Image=new ESImage(), Status=ComponentTypeStatus.Available},
                new ComponentType{ComponentName="Samsung Galaxy s7", ComponentInfo="", AdminComment="", Datasheet="", ImageUrl="",Manufacturer="",WikiLink="",Location="", Image=new ESImage(), Status=ComponentTypeStatus.Available},
                new ComponentType{ComponentName="LG G6", ComponentInfo="", AdminComment="", Datasheet="", ImageUrl="",Manufacturer="",WikiLink="",Location="", Image=new ESImage(), Status=ComponentTypeStatus.Available},
                new ComponentType{ComponentName="Lenovo 342", ComponentInfo="", AdminComment="", Datasheet="", ImageUrl="",Manufacturer="",WikiLink="",Location="", Image=new ESImage(), Status=ComponentTypeStatus.Available},
                new ComponentType{ComponentName="Dell XPS 1000", ComponentInfo="", AdminComment="", Datasheet="", ImageUrl="",Manufacturer="",WikiLink="",Location="", Image=new ESImage(), Status=ComponentTypeStatus.Available},
                new ComponentType{ComponentName="Dell Inspiron", ComponentInfo="", AdminComment="", Datasheet="", ImageUrl="",Manufacturer="",WikiLink="",Location="", Image=new ESImage(), Status=ComponentTypeStatus.ReservedAdmin}

            };


            foreach (ComponentType c in componentTypes)
            {
                context.ComponentTypes.Add(c);
            }

            context.SaveChanges();
            
            var components = new Component[]
            {
                new Component{ComponentTypeID=componentTypes.Single(ct=>ct.ComponentName=="Photon").ComponentTypeID,
                ComponentNumber=123, Status=ComponentStatus.ReservedAdmin, AdminComment="", SerialNo="123", UserComment="m", CurrentLoanInformationId=2612 },
                /*new Component{ComponentNumber=32, Status=ComponentStatus.Available, AdminComment="", SerialNo="32", UserComment="hdgm", CurrentLoanInformationId=1552 },
                new Component{ComponentNumber=54, Status=ComponentStatus.ReservedAdmin, AdminComment="", SerialNo="54", UserComment="mfdg", CurrentLoanInformationId=142 },
                new Component{ComponentNumber=234, Status=ComponentStatus.ReservedAdmin, AdminComment="", SerialNo="234", UserComment="mhg", CurrentLoanInformationId=112 },
                new Component{ComponentNumber=653, Status=ComponentStatus.ReservedAdmin, AdminComment="", SerialNo="653", UserComment="mdg", CurrentLoanInformationId=162 },
                new Component{ComponentNumber=676, Status=ComponentStatus.ReservedAdmin, AdminComment="", SerialNo="676", UserComment="mgh", CurrentLoanInformationId=142 }
                */

            };


            foreach (Component c in components)
            {
                context.Components.Add(c);
            }

            context.SaveChanges();
            
            var ctc = new ComponentTypeCategory[]
           {
                new ComponentTypeCategory{CategoryID = categories.Single(c=>c.Name=="IoT").CategoryID,
                    ComponentTypeID = componentTypes.Single(ct=> ct.ComponentName=="Photon").ComponentTypeID},
                new ComponentTypeCategory{CategoryID = categories.Single(c=>c.Name=="IoT").CategoryID,
                    ComponentTypeID = componentTypes.Single(ct=> ct.ComponentName=="Electron").ComponentTypeID},
                new ComponentTypeCategory{CategoryID = categories.Single(c=>c.Name=="Smartphone").CategoryID,
                    ComponentTypeID = componentTypes.Single(ct=> ct.ComponentName=="Samsung Galaxy s7").ComponentTypeID},
                new ComponentTypeCategory{CategoryID = categories.Single(c=>c.Name=="Smartphone").CategoryID,
                    ComponentTypeID = componentTypes.Single(ct=> ct.ComponentName=="LG G6").ComponentTypeID},
                new ComponentTypeCategory{CategoryID = categories.Single(c=>c.Name=="PC").CategoryID,
                    ComponentTypeID = componentTypes.Single(ct=> ct.ComponentName=="Dell XPS 1000").ComponentTypeID},
                new ComponentTypeCategory{CategoryID = categories.Single(c=>c.Name=="PC").CategoryID,
                    ComponentTypeID = componentTypes.Single(ct=> ct.ComponentName=="Dell Inspiron").ComponentTypeID},
                new ComponentTypeCategory{CategoryID = categories.Single(c=>c.Name=="PC").CategoryID,
                    ComponentTypeID = componentTypes.Single(ct=> ct.ComponentName=="Lenovo 342").ComponentTypeID}
           };

            
            foreach (ComponentTypeCategory c in ctc)
            {
                context.ComponentTypeCategories.Add(c);
            }

            context.SaveChanges();
            
        }


    }
}

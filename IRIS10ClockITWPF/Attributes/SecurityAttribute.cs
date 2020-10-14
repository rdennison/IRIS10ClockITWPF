using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace IrisClockITAttributes
{
    public sealed class SecurityAttribute : Attribute
    {
        private static Dictionary<string, Guid> _controllerGuids = null;
        private static Dictionary<string, Guid> _modelGuids = null;

        public string FkDescriptor { get; set; }
        public bool NotReportable { get; set; }

        public string Module { get; set; }
        public string Display { get; set; }
        public string ListValue { get; set; }

        public string Name { get; set; }

        public sealed class ModuleListData
        {
            public string Key { get; set; }
            public string Description { get; set; }

            public object BaseData { get; set; }
        }


        public enum Modules
        {
            AccountsPayable = 0,
            AccountsReceivable = 1,
            CostAccounting = 2,
            EquipmentManagement = 3,
            RoadInventory = 4,
            ServiceRequest = 5,
            SERVICES = 6,
            StreetWise = 7,
            VegetationManagement = 8,
            Utilities = 9
        }

        readonly static Dictionary<Modules, string> modulesLookup = new Dictionary<Modules, string>()
        {
            { Modules.AccountsPayable, "Accounts Payable" },
            { Modules.AccountsReceivable, "Accounts Receivable" },
            { Modules.CostAccounting, "Cost Accounting" },
            { Modules.EquipmentManagement, "Equipment Management" },
            { Modules.RoadInventory, "Road Inventory" },
            { Modules.ServiceRequest, "Service Request" },
            { Modules.SERVICES, "SERVICES" },
            { Modules.StreetWise, "Street Wise" },
            { Modules.VegetationManagement, "Vegetation Management" },
            { Modules.Utilities, "Utilities" }
        };


        public SecurityAttribute()
        {
            
        }


    }
}

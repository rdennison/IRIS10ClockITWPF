using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace IrisClockITAttributes
{
    public class ReportAttribute : Attribute
    {
        public string FkDescriptor { get; set; }
        public bool NotReportable { get; set; }

        public string Module { get; set; }
        public string Display { get; set; }
        public string ListValue { get; set; }

        public sealed class ModuleListData
        {
            public string Key { get; set; }
            public string Description { get; set; }

            public object BaseData { get; set; }
        }

        //TODO: TE Add the rest once I confirm these work
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
            VegetationManagement = 8
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
            { Modules.VegetationManagement, "Vegetation Management" }
        };

        public void GenerateLists()
        {
            //Dictionary<Modules, string> modules = modulesLookup;
            //Assembly a = Assembly.Load("IrisModels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            //IEnumerable<ModuleListData> moduleLista = new List<ModuleListData>();
            //IList<ModuleListData> moduleListb = new List<ModuleListData>();
            //IEnumerable<ModuleListData> aPLista = new List<ModuleListData>();
            //IList<ModuleListData> aPListb = new List<ModuleListData>();
            //IEnumerable<ModuleListData> aRLista = new List<ModuleListData>();
            //IList<ModuleListData> aRListb = new List<ModuleListData>();
            //IEnumerable<ModuleListData> cALista = new List<ModuleListData>();
            //IList<ModuleListData> cAListb = new List<ModuleListData>();
            //IEnumerable<ModuleListData> eMLista = new List<ModuleListData>();
            //IList<ModuleListData> eMListb = new List<ModuleListData>();
            //IEnumerable<ModuleListData> rILista = new List<ModuleListData>();
            //IList<ModuleListData> rIListb = new List<ModuleListData>();
            //IEnumerable<ModuleListData> sRLista = new List<ModuleListData>();
            //IList<ModuleListData> sRListb = new List<ModuleListData>();
            //IEnumerable<ModuleListData> sLista = new List<ModuleListData>();
            //IList<ModuleListData> sListb = new List<ModuleListData>();
            //IEnumerable<ModuleListData> sWLista = new List<ModuleListData>();
            //IList<ModuleListData> sWListb = new List<ModuleListData>();
            //IEnumerable<ModuleListData> vMLista = new List<ModuleListData>();
            //IList<ModuleListData> vMListb = new List<ModuleListData>();

            //foreach (var m in modules)
            //{
            //    ModuleListData mods = new ModuleListData();
            //    mods.Key = m.Key.ToString();
            //    mods.Description = m.Value;
            //    moduleListb.Add(mods);
            //}

            //moduleLista = moduleListb.OrderBy(o => o.Description);
            //SelectList moduleList = new SelectList(moduleLista, "Key", "Description");
            //HttpRuntime.Cache.Add("ReportModuleList", moduleList, null, DateTime.Now.AddDays(14), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

            //foreach (TypeInfo ti in a.DefinedTypes)
            //{
            //    if (ti.GetCustomAttribute<ReportAttribute>() != null)
            //    {
            //        switch (ti.GetCustomAttribute<ReportAttribute>().Module)
            //        {
            //            case "Accounts Payable":
            //                ModuleListData modsap = new ModuleListData();
            //                modsap.Key = ti.GetCustomAttribute<ReportAttribute>().ListValue;
            //                modsap.Description = ti.GetCustomAttribute<ReportAttribute>().Display;
            //                aPListb.Add(modsap);
            //                break;
            //            case "Accounts Receivable":
            //                ModuleListData modsar = new ModuleListData();
            //                modsar.Key = ti.GetCustomAttribute<ReportAttribute>().ListValue;
            //                modsar.Description = ti.GetCustomAttribute<ReportAttribute>().Display;
            //                aRListb.Add(modsar);
            //                break;
            //            case "Cost Accounting":
            //                ModuleListData modsca = new ModuleListData();
            //                modsca.Key = ti.GetCustomAttribute<ReportAttribute>().ListValue;
            //                modsca.Description = ti.GetCustomAttribute<ReportAttribute>().Display;
            //                cAListb.Add(modsca);
            //                break;
            //            case "Equipment Management":
            //                ModuleListData modsem = new ModuleListData();
            //                modsem.Key = ti.GetCustomAttribute<ReportAttribute>().ListValue;
            //                modsem.Description = ti.GetCustomAttribute<ReportAttribute>().Display;
            //                eMListb.Add(modsem);
            //                break;
            //            case "Road Inventory":
            //                ModuleListData modsri = new ModuleListData();
            //                modsri.Key = ti.GetCustomAttribute<ReportAttribute>().ListValue;
            //                modsri.Description = ti.GetCustomAttribute<ReportAttribute>().Display;
            //                rIListb.Add(modsri);
            //                break;
            //            case "Service Request":
            //                ModuleListData modssr = new ModuleListData();
            //                modssr.Key = ti.GetCustomAttribute<ReportAttribute>().ListValue;
            //                modssr.Description = ti.GetCustomAttribute<ReportAttribute>().Display;
            //                sRListb.Add(modssr);
            //                break;
            //            case "SERVICES":
            //                ModuleListData modss = new ModuleListData();
            //                modss.Key = ti.GetCustomAttribute<ReportAttribute>().ListValue;
            //                modss.Description = ti.GetCustomAttribute<ReportAttribute>().Display;
            //                sListb.Add(modss);
            //                break;
            //            case "Street Wise":
            //                ModuleListData modssw = new ModuleListData();
            //                modssw.Key = ti.GetCustomAttribute<ReportAttribute>().ListValue;
            //                modssw.Description = ti.GetCustomAttribute<ReportAttribute>().Display;
            //                sWListb.Add(modssw);
            //                break;
            //            case "Vegetation Management":
            //                ModuleListData modsvm = new ModuleListData();
            //                modsvm.Key = ti.GetCustomAttribute<ReportAttribute>().ListValue;
            //                modsvm.Description = ti.GetCustomAttribute<ReportAttribute>().Display;
            //                vMListb.Add(modsvm);
            //                break;
            //        }
            //    }
            //}
            //aPLista = aPListb.OrderBy(o => o.Description);
            //aRLista = aRListb.OrderBy(o => o.Description);
            //cALista = cAListb.OrderBy(o => o.Description);
            //eMLista = eMListb.OrderBy(o => o.Description);
            //rILista = rIListb.OrderBy(o => o.Description);
            //sRLista = sRListb.OrderBy(o => o.Description);
            //sLista = sListb.OrderBy(o => o.Description);
            //sWLista = sWListb.OrderBy(o => o.Description);
            //vMLista = vMListb.OrderBy(o => o.Description);
            //SelectList aRList = new SelectList(aRLista, "Key", "Description");
            //SelectList aPList = new SelectList(aPLista, "Key", "Description");
            //SelectList cAList = new SelectList(cALista, "Key", "Description");
            //SelectList eMList = new SelectList(eMLista, "Key", "Description");
            //SelectList rIList = new SelectList(rILista, "Key", "Description");
            //SelectList sRList = new SelectList(sRLista, "Key", "Description");
            //SelectList sList = new SelectList(sLista, "Key", "Description");
            //SelectList sWList = new SelectList(sWLista, "Key", "Description");
            //SelectList vMList = new SelectList(vMLista, "Key", "Description");
            //HttpRuntime.Cache.Add("AccountsPayableList", aPList, null, DateTime.Now.AddDays(14), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
            //HttpRuntime.Cache.Add("AccountsReceivableList", aRList, null, DateTime.Now.AddDays(14), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
            //HttpRuntime.Cache.Add("CostAccountingList", cAList, null, DateTime.Now.AddDays(14), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
            //HttpRuntime.Cache.Add("EquipmentManagementList", eMList, null, DateTime.Now.AddDays(14), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
            //HttpRuntime.Cache.Add("RoadInventoryList", rIList, null, DateTime.Now.AddDays(14), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
            //HttpRuntime.Cache.Add("ServiceRequestList", sRList, null, DateTime.Now.AddDays(14), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
            //HttpRuntime.Cache.Add("SERVICESList", sList, null, DateTime.Now.AddDays(14), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
            //HttpRuntime.Cache.Add("StreetWiseList", sWList, null, DateTime.Now.AddDays(14), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
            //HttpRuntime.Cache.Add("VegetationManagementList", vMList, null, DateTime.Now.AddDays(14), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

        }
    }
}

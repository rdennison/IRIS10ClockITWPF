
using IrisClockITAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using System.Text;

namespace IRIS10ClockITWPF.Models
{
    public class ModelBase : SqlBase
    {
        [UIHint("IRISDate")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm:ss:fffzzz}")]
        [Display(Name = "Created Date")]
        [IrisGridColumn(Hidden = true, Format = "{0:MM/dd/yyyy HH:mm:ss:fffzzz}")]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = false)]
        [DbProperties(DatabaseType = SqlDbType.DateTime2)]
        public DateTime? CreatedDate { get; set; }

        [ForeignKey(typeof(IRISUserModel), ForeignKeyDisplayField = "NameDesc")]
        [Display(Name = "Created By")]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        [CantEdit]
        [IrisGridColumn(Hidden = true)]
        [DbProperties(DatabaseType = SqlDbType.Int)]
        public int? CreatedByUser_Key { get; set; }

        [UIHint("IRISDate")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm:ss:fffzzz}")]
        [Display(Name = "Updated Date")]
        [IrisGridColumn(Hidden = true, Format = "{0:MM/dd/yyyy HH:mm:ss:fffzzz}")]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = false)]
        [DbProperties(DatabaseType = SqlDbType.DateTime2)]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey(typeof(IRISUserModel), ForeignKeyDisplayField = "NameDesc")]
        [Display(Name = "Updated By")]
        [CantEdit]
        [IrisGridColumn(Hidden = true)]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        [DbProperties(DatabaseType = SqlDbType.Int)]
        public int? UpdatedByUser_Key { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [ForeignKey(typeof(TenantModel), ForeignKeyDisplayField = "TenantName")]
        [Display(Name = "Tenant")]
        [CantEdit]
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
        [DbProperties(DatabaseType = SqlDbType.Int)]
        public int Tenant_Key { get; set; }

        private static List<Type> modelCache = null;

        public static List<Type> ModelTypes
        {
            get
            {
                if (modelCache == null)
                {
                    Assembly a = Assembly.GetExecutingAssembly();
                    Type[] allTypes = a.GetTypes();

                    modelCache = new List<Type>();
                    foreach (Type t in allTypes)
                    {
                        if (t.FullName.StartsWith("IrisModels.Models"))
                            modelCache.Add(t);
                    }
                }

                return modelCache;
            }
        }

    }
}

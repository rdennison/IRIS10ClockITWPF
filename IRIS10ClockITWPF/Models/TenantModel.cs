
using IrisClockITAttributes;
using IrisUI.GridBuilder.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IRIS10ClockITWPF.Models
{
    [ModelDataBindings(DatabaseName = "IrisAuth", KeyFieldName = "Tenant_Key", TableName = "Tenant")]
    public sealed class TenantModel
    {
        [DBKey]
        [DbProperties(DatabaseType = SqlDbType.Int)]
        public int Tenant_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Text = true)]
		[Display(Name = "Tenant Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string TenantName { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "IRIS 09 DB")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string IRIS09DB { get; set; }

		[DbProperties(DatabaseType = SqlDbType.NVarChar, Size = 256)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Text = true)]
		[Display(Name = "username")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string username { get; set; }

    }
}
using IrisClockITAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IRIS10ClockITWPF.Models
{
    //[IrisGrid("~/API/Config/Read", "~/API/Config/Create", "~/API/Config/Update", "~/API/Config/Destroy")]
    public sealed class Config  : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your Config_Key is required.")]
		[IrisGridColumn(Width = 150)]
		public int REMOTEConfig_Key { get; set; }
		 
		[DBKey] 
		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[IrisGridColumn(Width = 150)]
		public Int64? Config_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Float, Precision = 53, Scale = 0)]
		[DataType("Decimal")]
		[FilterType(Number = true)]
		[Display(Name = "Last ID")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public decimal? LastID { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 255)]
		[FilterType(Text = true)]
		[Display(Name = "Support Information")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string SupportInformation { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "Phone Support Num")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string PhoneSupportNum { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "Fax Support Num")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string FaxSupportNum { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "Support E Mail Address")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string SupportEMailAddress { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 255)]
		[FilterType(Text = true)]
		[Display(Name = "Support Address")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string SupportAddress { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 1)]
		[FilterType(Text = true)]
		[Display(Name = "Employee Rate Default")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string EmployeeRateDefault { get; set; }

		[DbProperties(DatabaseType = SqlDbType.SmallInt)]
		[DataType("Short")]
		[Display(Name = "OH Calc Method")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public short? OHCalcMethod { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "SMTP Domain Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string SMTPDomainName { get; set; }

		[DbProperties(DatabaseType = SqlDbType.SmallInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Short")]
		[Display(Name = "Roads Leading Zeros")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public short RoadsLeadingZeros { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 20)]
		[FilterType(Text = true)]
		[Display(Name = "Default Budget No")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string DefaultBudgetNo { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 256)]
		[FilterType(Text = true)]
		[Display(Name = "Default Document Path")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string DefaultDocumentPath { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "Default SRS Email From Address")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string DefaultSRSEmailFromAddress { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "Enable Error Reporting")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte EnableErrorReporting { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "MMS Include Project Cost")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte MMSIncludeProjectCost { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "Security Enabled")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte SecurityEnabled { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "CRW Security Enabled")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte CRWSecurityEnabled { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "Post Billable Timecards To ARS")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte PostBillableTimecardsToARS { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "Auto Login")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte AutoLogin { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "Show Error Form")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte ShowErrorForm { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "Enable Beep Sound")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte EnableBeepSound { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "Enable Street Wise")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte EnableStreetWise { get; set; }

		[DbProperties(DatabaseType = SqlDbType.SmallInt)]
		[DataType("Short")]
		[Display(Name = "Projection")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public short? Projection { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "Set All Security To Admin")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte SetAllSecurityToAdmin { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Bit)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Boolean")]
		[FilterType(Dropdown2 = true)]
		[Display(Name = "Equipment Depreciation Uses Cas Hours")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public bool EquipmentDepreciationUsesCasHours { get; set; }

		
		public string RemoteAction { get; set; }

    }
}

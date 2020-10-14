
using IrisClockITAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IRIS10ClockITWPF.Models
{
    //[IrisGrid("~/API/IRISUser/Read", "~/API/IRISUser/Create", "~/API/IRISUser/Update", "~/API/IRISUser/Destroy")]
    public sealed class IRISUserModel 
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your User_Key is required.")]
		[IrisGridColumn(Width = 150)]
		public int REMOTEUser_Key { get; set; }
		 
		[DBKey] 
		[DbProperties(DatabaseType = SqlDbType.Int)]
		[DataType("Integer")]
		[IrisGridColumn(Width = 150)]
		public int? User_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[DataType("Integer")]
		[FilterType(Number = true)]
		[Display(Name = "Default Tenant Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public int? DefaultTenant_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.NVarChar, Size = 128)]
		[FilterType(Text = true)]
		[Display(Name = "Origin DB")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string OriginDB { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Char, Size = 10)]
		[FilterType(Text = true)]
		[Display(Name = "Security User Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string SecurityUser_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Text = true)]
		[Display(Name = "Login Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string LoginName { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Text = true)]
		[Display(Name = "User Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string UserName { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[FilterType(Number = true)]
		[Display(Name = "Employee Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public Int64? Employee_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "First Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string FirstName { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "Last Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string LastName { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 101)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IsExcludeSql]
		[FilterType(Text = true)]
		[Display(Name = "Name Desc")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string NameDesc { get; set; }

		//[DbProperties(DatabaseType = SqlDbType.VarBinary, Size = -1)]
		//TODO:  Rexamine at a later date in order to determine a data type for varbinary
		//public varbinary? Signature { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 128)]
		[FilterType(Text = true)]
		[Display(Name = "Email")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Email { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "Active")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte Active { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = -1)]
		[FilterType(Text = true)]
		[Display(Name = "SALT")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string SALT { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "Auth GUID")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string AuthGUID { get; set; }

		[DbProperties(DatabaseType = SqlDbType.DateTime2)]
		[UIHint("IRISDate")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
		[FilterType(Date = true)]
		[Display(Name = "Auth Date")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public DateTime? AuthDate { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = -1)]
		[FilterType(Text = true)]
		[Display(Name = "Hash Password")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string HashPassword { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Integer")]
		[FilterType(Number = true)]
		[Display(Name = "General Access Level")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public int GeneralAccessLevel { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Bit)]
		[DataType("Boolean")]
		[FilterType(Dropdown2 = true)]
		[Display(Name = "Login Change Password")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public bool? LoginChangePassword { get; set; }

		
		public string RemoteAction { get; set; }

    }
}


using IrisClockITAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;

namespace IRIS10ClockITWPF.Models
{
	public sealed class OrganizationModel : ModelBase
	{
		[Key]
		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IrisGridColumn(Width = 150)]
		public Int64 Organization_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Text = true)]
		[Display(Name = "Organization Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string OrganizationName { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IsExcludeSql]
		[FilterType(Text = true)]
		[Display(Name = "Name Desc")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string NameDesc { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "Address 1")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Address1 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "Address 2")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Address2 { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 30)]
		[FilterType(Text = true)]
		[Display(Name = "City")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string City { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 2)]
		[FilterType(Text = true)]
		[Display(Name = "State")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string State { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 10)]
		[FilterType(Text = true)]
		[Display(Name = "Zip Code")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string ZipCode { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 14)]
		[FilterType(Text = true)]
		[Display(Name = "Phone Number")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string PhoneNumber { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 14)]
		[FilterType(Text = true)]
		[Display(Name = "Fax Number")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string FaxNumber { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "E Mail")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string EMail { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "Website")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Website { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 10)]
		[FilterType(Text = true)]
		[Display(Name = "UNC Caller ID")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string UNCCallerID { get; set; }

		//[DbProperties(DatabaseType = SqlDbType.VarBinary, Size = -1)]
		//TODO:  Rexamine at a later date in order to determine a data type for varbinary
		//public varbinary? Logo { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 256)]
		[FilterType(Text = true)]
		[Display(Name = "Comments")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Comments { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "Default Organization")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte DefaultOrganization { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 2)]
		[FilterType(Text = true)]
		[Display(Name = "ARS Invoice Number Prefix")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string ARSInvoiceNumberPrefix { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[DataType("Byte")]
		[Display(Name = "Active")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte Active { get; set; }

	}
}

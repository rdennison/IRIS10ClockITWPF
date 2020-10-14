
using IrisClockITAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;

namespace IRIS10ClockITWPF.Models
{
	public sealed class UOMModel : ModelBase
	{
		[Key]
		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IrisGridColumn(Width = 150)]
		public Int64 UOM_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 20)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Text = true)]
		[Display(Name = "English Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string EnglishName { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 20)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[FilterType(Text = true)]
		[Display(Name = "Metric Name")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string MetricName { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "English Conversion Factor")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string EnglishConversionFactor { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 50)]
		[FilterType(Text = true)]
		[Display(Name = "Metric Conversion Factor")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string MetricConversionFactor { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 20)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IsExcludeSql]
		[FilterType(Text = true)]
		[Display(Name = "Name Desc")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string NameDesc { get; set; }

	}
}

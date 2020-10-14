using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisClockITAttributes
{
    public sealed class AddSpecialColumnAttribute : Attribute
    {
        public string ColumnName { get; set; }
        public string ButtonName { get; set; }
    }
}

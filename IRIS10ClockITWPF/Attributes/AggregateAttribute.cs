using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisClockITAttributes
{
    public sealed class AggregateAttribute : Attribute
    {
        public bool AllowSum { get; set; }
        public bool AllowCount { get; set; }
        public bool AllowMin { get; set; }
        public bool AllowMax { get; set; }
        public bool AllowAvg { get; set; }
    }
}

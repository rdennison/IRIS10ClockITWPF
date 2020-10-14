using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IrisClockITAttributes
{
    public sealed class CalculatedFieldAttribute : Attribute
    {
        public string RateType { get; set; }

        public bool Init { get; set; }
        
    }
}

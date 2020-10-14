using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisClockITAttributes
{
    /// <summary>
    /// This attribute is used when a property that is a autonumber field
    /// </summary>
    /// 
    public sealed class IsAutoNumberAttribute : System.Attribute
    {
        public bool Exclude = false;

        public IsAutoNumberAttribute()
        {
            Exclude = true;
        }
    }
}

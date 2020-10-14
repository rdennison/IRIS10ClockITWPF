using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisClockITAttributes
{
    public class EditorTypeAttribute : Attribute
    {
        public bool PopOut { get; set; }
        public bool InLine { get; set; }
        public bool InCell { get; set; }
        public EditorTypeAttribute()
        {
            PopOut = false;
            InLine = false;
            InCell = false;
        }
    }
}

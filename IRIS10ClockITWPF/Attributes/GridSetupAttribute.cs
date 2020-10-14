using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisClockITAttributes
{
    public sealed class GridSetupAttribute : Attribute
    {
        public int GridHeight { get; set; }

        public bool Selectable { get; set; }
        
        public bool CheckBoxColumn { get; set; }

        public string SpecialButton { get; set; }
        public int SecurityLevel { get; set; }
    }
}

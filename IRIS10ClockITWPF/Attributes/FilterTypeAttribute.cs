using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisClockITAttributes
{
   public class FilterTypeAttribute : Attribute
    {
  
        public bool Dropdown { get; set; }

        public bool Date { get; set; }

        public bool Text { get; set; }

        public bool Number { get; set; }

        public bool Dropdown2 { get; set;  }

        public bool Operator2 { get; set; }

        public bool Group1 { get; set; }

        public bool Group2 { get; set; }



   
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisClockITAttributes
{
    /// <summary>
    /// This attribute is used when a property is used in the Order By of the Read().   The Index is used when you have 
    /// multiple fields in the Order By.   0 is the first field to Order By, 1 is the 2nd field to Order By
    /// </summary>
    public sealed class OrderByFieldAttribute : System.Attribute
    {
        public Boolean OrderByAssending { get; set; }
        public int Index { get; set; }

        public OrderByFieldAttribute(Boolean orderbyassending = true, int index = -1)
        {
            OrderByAssending = orderbyassending;
            Index = index;
        }
    }
}

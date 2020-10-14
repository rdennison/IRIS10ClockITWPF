using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrisUI.GridBuilder.Attributes
{
    public sealed class ModelDataBindingsAttribute : Attribute
    {
        public string DatabaseName { get; set; }
        public string TableName { get; set; }
        public string KeyFieldName { get; set; }
    }
}

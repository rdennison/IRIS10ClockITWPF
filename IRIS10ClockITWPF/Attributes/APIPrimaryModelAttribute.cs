using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisClockITAttributes
{
    public sealed class APIPrimaryModelAttribute : Attribute
    {
        public Type ModelType { get; set; }
        public APIPrimaryModelAttribute(Type t) { ModelType = t; }

    }
}

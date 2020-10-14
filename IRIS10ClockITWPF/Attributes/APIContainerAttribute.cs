using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisClockITAttributes
{
    public sealed class APIContainerAttribute : Attribute
    {
        public Type BaseModel;
        public string DisplayName { get; set; }

        public APIContainerAttribute(Type t)
        {

            BaseModel = t;
          
        }


    }
}

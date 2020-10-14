using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace IrisClockITAttributes
{
    public sealed class ForeignKeyReference
    {
        public string Key { get; set; }
        public string Description { get; set; }
        public object BaseData { get; set; }
    }

    public sealed class ForeignKeyAttribute : Attribute
    {
        private static List<Type> serviceTypes = null;
        public static Type GetServiceTypeFor(Type objectType)
        {
            if (serviceTypes == null)
            {
                serviceTypes = new List<Type>();
                Assembly a = Assembly.Load("IrisUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
                
                foreach (TypeInfo ti in a.DefinedTypes)
                {
                    if (ti.FullName.StartsWith("IrisUI.Services"))
                        serviceTypes.Add(ti.AsType());
                }
            }

            foreach (Type t in serviceTypes)
            {
                if (t.Name.EndsWith("Service") && t.BaseType.GenericTypeArguments.Length == 1 && t.BaseType.GenericTypeArguments[0] == objectType)
                    return t;
            }

            return null;
        }

        public Dictionary<string, string> Settings { get; set; }
        public string ForeignKeyDisplayField { get; set; }
        public Type ReferenceModelType { get; set; }
        public string AdditionalText { get; set; }
        public bool EnableFiltering { get; set; }

        public ForeignKeyAttribute(Type t) { ReferenceModelType = t;}
    }
}

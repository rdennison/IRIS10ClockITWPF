using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IrisClockITAttributes
{
   public sealed class APIForeignRelationAttribute : Attribute
    {
        public string PrimaryTableKeyName { get; set; }
        public string ForeignKeyTableName { get; set; }
        public Type ModelType { get; set; }

        public APIForeignRelationAttribute(Type t, PropertyInfo primaryKey, PropertyInfo foreignKey) {

            ModelType = t;
            PrimaryTableKeyName = primaryKey.Name;
            ForeignKeyTableName = foreignKey.Name;
        }
        public APIForeignRelationAttribute(Type t, PropertyInfo primaryKey)
        {

            ModelType = t;
            PrimaryTableKeyName = primaryKey.Name;
            ForeignKeyTableName = primaryKey.Name;
        }

        public APIForeignRelationAttribute(Type t, Type primaryModel)
        {

            ModelType = t;
            foreach (PropertyInfo p in primaryModel.GetProperties())
            {
                if (p.GetCustomAttribute<KeyAttribute>() != null)
                {
                    PrimaryTableKeyName = p.Name;
                    ForeignKeyTableName = p.Name;
                    break;
                }

            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisClockITAttributes
{
    public sealed class IrisMenuAttribute : Attribute
    {
        public string Category { get; set; }
        public string Path { get; set; }
        public int Weight { get; set; }
        public string OnClick { get; set; }

        public IrisMenuAttribute(string path, int weight = 0, string onClick = null)
        {
            Category = "Main";
            Path = path;
            Weight = weight;
            OnClick = onClick;
        }

        public string[] GetPathChain()
        {
            return Path.Split('/', '\\');
        }
    }
}

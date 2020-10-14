using System;
using IrisClockITAttributes.Icons;

namespace IrisClockITAttributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class IrisToolbarAttribute : Attribute
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string OnClick { get; set; }
        public string ToolTipText { get; set; }
        public string Group { get; set; }
        public string ImagePath { get; set; }
        public int Order { get; set; }
        public int GroupOrder { get; set; }
        public string Template { get; set; }

        public BootstrapGlyphiconType IconType { get; set; }
        public bool IsDivName { get; set; }

        public int SecurityLevel { get; set; }
        
        public IrisToolbarAttribute()
        {
            SecurityLevel = 2;
        }
    }
}

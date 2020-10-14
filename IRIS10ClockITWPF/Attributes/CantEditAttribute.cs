using System;
namespace IrisClockITAttributes
{
    public sealed class CantEditAttribute : Attribute
    {
        public bool ShowInEditor { get; set; }
        
    }
}

using System;

namespace IrisClockITAttributes
{
    public sealed class HasChildGridAttribute : Attribute
    {
        public Type ChildModel { get; set; }
    }
}

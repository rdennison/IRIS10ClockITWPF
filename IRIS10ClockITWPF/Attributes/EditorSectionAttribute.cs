using System;

namespace IrisClockITAttributes
{
    public sealed class EditorSectionAttribute : Attribute
    {
        public string SectionName { get; set; }
        public bool Hidden { get; set; }
    }
}

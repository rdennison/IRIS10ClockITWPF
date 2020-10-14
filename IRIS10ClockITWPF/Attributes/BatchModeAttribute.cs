using System;

namespace IrisClockITAttributes
{
    public class BatchModeAttribute : Attribute
    {
        public string DataBoundEvent { get; set; }
        public bool StartInEdit { get; set; }
    }
}

using System;

namespace IrisClockITAttributes
{
    public enum AggregateMode
    {
        None,
        Sum,
        Average,
        Min,
        Max
    }

    public enum DefaultValueSlug
    {
        DateTimeNow = 1
    }

    public sealed class IrisGridColumnAttribute : Attribute
    {
        public static readonly IrisGridColumnAttribute Default = new IrisGridColumnAttribute();

        public int Width { get; set; }
        public string Format { get; set; }
        public AggregateMode Aggregate { get; set; }
        public bool ReadOnly { get; set; }
        public object DefaultValue { get; set; }
        public bool Hidden { get; set; }
        public bool IncludeInMenu { get; set; }
        public string ScreenName { get; set; }
        public bool Grouping { get; set; }
        public bool ColumnFilter { get; set; }

        public IrisGridColumnAttribute()
        {
            Width = 120;
            Aggregate = AggregateMode.None;
            Hidden = false;
            IncludeInMenu = true;
            Grouping = true;
            ColumnFilter = true;
        }

        public object GetDefaultValue()
        {
            if (DefaultValue is DefaultValueSlug)
            {
                DefaultValueSlug slug = (DefaultValueSlug) DefaultValue;

                switch (slug)
                {
                    case DefaultValueSlug.DateTimeNow:
                        return DateTime.Now;
                }
            }

            return DefaultValue;
        }
    }
}

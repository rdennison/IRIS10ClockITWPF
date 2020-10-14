using System;

namespace IrisClockITAttributes
{
    public sealed class LandingMenuAttribute : Attribute
    {
        public string Path { get; set; }
        public string OnClick { get; set; }

        public LandingMenuAttribute(string path, string onClick = null)
        {
            Path = path;
            OnClick = onClick;
        }

        public string[] GetPathChain()
        {
            return Path.Split('/', '\\');
        }
    }
}

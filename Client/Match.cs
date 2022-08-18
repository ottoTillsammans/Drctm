using System;
using System.Collections.Generic;
using System.Linq;

namespace Client
{
    public static class Match
    {
        private static Dictionary<string, string> Tags = new Dictionary<string, string>();

        public static string GetTag(string type)
        {
            return Tags
                .Select(t => t.Key == type)
                .Any() ? Tags[type] : String.Empty;
        }

        public static void SetTag(string type, string tag)
        {
            if (!Tags.Select(t => t.Key == type).Any())
                Tags.Add(type, tag);
        }
    }
}

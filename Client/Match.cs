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
            return Tags[type];
        }

        public static void SetTag(string type, string tag)
        {
            Tags[type] = tag;
        }
    }
}

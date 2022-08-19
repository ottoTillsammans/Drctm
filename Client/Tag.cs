using System;
using System.Collections.Generic;
using System.Linq;

namespace Client
{
    public static class Tag
    {
        private static Dictionary<string, string> Tags = new Dictionary<string, string>();

        public static string GetTag(string type)
        {
            return Tags
                .Select(t => t.Key == type)
                .Any() ? Tags[type] : String.Empty;
        }

        public static void AddTag(string type, string tag)
        {
            if (!Tags.Select(t => t.Key == type).Any())
                Tags.Add(type, tag);
        }

        public static string MakeOpeningTag(this string name) => string.Format("<{0}>", name);

        public static string MakeClosingTag(this string name) => string.Format("</{0}>", name);

        public static string AddAttribute(this string element, string attrName, string attrValue)
        {
            string result = element;

            if (!string.IsNullOrWhiteSpace(element) &&
                !string.IsNullOrWhiteSpace(attrName) &&
                !string.IsNullOrWhiteSpace(attrValue))
                result = element.Insert(element.Length - 1, string.Format(" {0}=\"{1}\"", attrName, attrValue));

            return result;
        }

        public static string AddHtmlTag(this string document)
        {
            string opening = "<html>";
            string closing = "</html>";
            
            return string.Format("{0}\n{1}\n{2}", opening, document, closing);
        }
    }
}

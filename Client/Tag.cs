using System;
using System.Collections.Generic;
using System.Linq;

namespace Client
{
    public static class Tag
    {
        /// <summary>
        /// Dictionary to store pairs JSON type - HTML tag.
        /// </summary>
        private static Dictionary<string, string> Tags = new Dictionary<string, string>();

        /// <summary>
        /// Get tag by it's JSON type.
        /// </summary>
        /// <param name="type">JSON type.</param>
        /// <returns>Tag.</returns>
        public static string GetTag(string type)
        {
            return Tags
                .Select(t => t.Key == type)
                .Any() ? Tags[type] : String.Empty;
        }

        /// <summary>
        /// Add new pair type - tag to dictionary.
        /// </summary>
        /// <param name="type">JSON type.</param>
        /// <param name="tag">HTML tag.</param>
        public static void AddTag(string type, string tag)
        {
            if (!Tags.Select(t => t.Key == type).Any())
                Tags.Add(type, tag);
        }

        /// <summary>
        /// Make opening tag from name.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <returns>Opening tag.</returns>
        public static string MakeOpeningTag(this string name) => string.Format("<{0}>", name);

        /// <summary>
        /// Make closing tag from name.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <returns>Closing tag.</returns>
        public static string MakeClosingTag(this string name) => string.Format("</{0}>", name);

        /// <summary>
        /// Add attribute into a tag.
        /// </summary>
        /// <param name="attribute">Attribute</param>
        /// <param name="attrName">Name.</param>
        /// <param name="attrValue">Value.</param>
        /// <returns>Tag with attribute.</returns>
        public static string AddAttribute(this string attribute, string attrName, string attrValue)
        {
            string result = attribute;

            if (!string.IsNullOrWhiteSpace(attribute) &&
                !string.IsNullOrWhiteSpace(attrName) &&
                !string.IsNullOrWhiteSpace(attrValue))
                result = attribute.Insert(attribute.Length - 1, string.Format(" {0}=\"{1}\"", attrName, attrValue));

            return result;
        }

        /// <summary>
        /// Add any outter tags to document.
        /// </summary>
        /// <param name="document">HTML document.</param>
        /// <param name="opening">Opening tag.</param>
        /// <param name="closing">Closing tag.</param>
        /// <returns>Document with tags.</returns>
        public static string AddOutterTags(this string document, string opening, string closing)
        {
            return string.Format("{0}\n{1}\n{2}", opening, document, closing);
        }

        /// <summary>
        /// Get any block from document by its starting and ending substrings.
        /// </summary>
        /// <param name="rawForm">Document.</param>
        /// <param name="startStr">Starting substring.</param>
        /// <param name="endStr">Ending substring.</param>
        /// <param name="searchLast">True if need seach last occurrence of ending substring, otherwise false.</param>
        /// <returns></returns>
        public static string GetBlock(this string rawForm, string startStr, string endStr, bool searchLast)
        {
            int start = rawForm.IndexOf(startStr);
            int end = searchLast ? rawForm.LastIndexOf(endStr) : rawForm.IndexOf(endStr);

            if (start != -1 && end != -1)
                return rawForm.Substring(start, end);
            else
                return string.Empty;
        }
    }
}

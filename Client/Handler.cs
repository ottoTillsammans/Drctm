using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Client
{
    public static class Handler
    {
        /// <summary>
        /// Get JObject prom file.
        /// </summary>
        /// <param name="path">Path to file.</param>
        /// <returns>JObject or null if path is invalid.</returns>
        public static JObject? GetJObject(string path)
        {
            JObject? jObj = null;

            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    try
                    {
                        jObj = JObject.Parse(reader.ReadToEnd());
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return jObj;
        }
    }
}

using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Client
{
    public static class Program
    {
        public static void Main()
        {
            #region Tests.

            Tag.AddTag("filler", "");
            Tag.AddTag("text", "");
            Tag.AddTag("textarea", "textarea");
            Tag.AddTag("checkbox", "");
            Tag.AddTag("button", "");
            Tag.AddTag("select", "select");
            Tag.AddTag("radio", "");

            //Console.WriteLine("Введите путь до файла");
            //var path1 = Console.ReadLine();
            //var path2 = Console.ReadLine();

            var jObj1 = Handler.GetJObject("E:\\Repository\\json\\form1.json");
            var jObj2 = Handler.GetJObject("E:\\Repository\\json\\form2.json");

            //Console.WriteLine(jObj1.ToString());
            //Console.WriteLine();
            //Console.WriteLine(jObj2.ToString());
            //Console.WriteLine();

            var array = jObj1.ToString().ToCharArray();
            var str = jObj1.ToString().Replace("{", "<test>").Replace("}", "</test>").Replace("[", " ").Replace("]", " ").Replace(",", " ");
            Console.WriteLine(str);

            Console.ReadKey();

            #endregion
        }
    }
}
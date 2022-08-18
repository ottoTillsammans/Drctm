using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Tag
    {
        private string Name { get; set; }

        public Tag(string name)
        {
            Name = name;
        }

        public string Open() => string.Format("<{0}>", Name);

        public string Close() => string.Format("</{0}>", Name);
    }
}

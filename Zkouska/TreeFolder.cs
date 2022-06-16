using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zkouska
{
    public class TreeFolder<T> : TreeObject<T>
    {
        public List<TreeObject<T>>? Entries { get; set; }

        public TreeFolder()
        {
            Entries = new List<TreeObject<T>>();
        }

        public override string? ToString()
        {
            string result = "";
            for (int i = 0; i < this.delkaCesty; i++)
            {
                result += "  ";
            }
            result += this.Name + "\n";
            foreach (var entry in Entries)
            {
                result += entry.ToString();
            }
            return result;
        }
    }
}

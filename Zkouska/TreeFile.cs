using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zkouska
{
    public class TreeFile<T> : TreeObject<T>
    { 
        public T? Data { get; set; }

        public TreeFile()
        {

        }

        public override string? ToString()
        {
            string s = "";
            for(int i = 0; i < this.delkaCesty; i++)
            {
                s += "  ";
            }
            return s + this.Name + " | " + this.Data + "\n";
        }
    }
}

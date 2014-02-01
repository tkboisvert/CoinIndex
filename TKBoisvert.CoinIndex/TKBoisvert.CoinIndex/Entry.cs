using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKBoisvert.CoinIndex
{
    public class Entry
    {
        public static void Add(string dataEntry, string date, string path)
        {
            using(var sw = new StreamWriter(path + @"\" + dataEntry + ".ci"))
            {
                sw.Write(date);
            }
        }
    }
}

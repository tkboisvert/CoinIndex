using System.IO;

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

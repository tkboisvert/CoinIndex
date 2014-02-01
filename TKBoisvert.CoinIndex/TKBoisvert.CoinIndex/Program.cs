using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKBoisvert.CoinIndex
{
    public class Program
    {
        static void Main(string[] args)
        {
            Entry.Add("10101110101001", "1980", @"C:\\Users\Admin\Desktop\Programwork\Files\Top");

            var getHashTableContents = 
                new GetHashTableContents(@"C:\\Users\Admin\Desktop\Programwork\Files\Top");

            var coinHashTable = new CoinHashTable(getHashTableContents.LoadFiles());

            Console.WriteLine(coinHashTable._CoinHash["10101110101001"]);

            Console.Read();

        }
    }
}

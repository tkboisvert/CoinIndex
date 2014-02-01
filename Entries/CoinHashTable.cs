using System.Collections;

namespace TKBoisvert.CoinIndex
{
    /// <summary>
    /// Uses a previously fabricated jagged array to load a Hashtable.
    /// </summary>
    class CoinHashTable
    {
        public Hashtable _CoinHash;

        private string[][] _CoinIndexArray;

        public CoinHashTable(string[][] arrayFromUser)
        {
            _CoinIndexArray = arrayFromUser;

            _CoinHash = new Hashtable();

            LoadTable();
        }

        private void LoadTable()
        {
            foreach (var array in _CoinIndexArray)
            {
                _CoinHash[array[0]] = array[1];
            }
        }
    }
}

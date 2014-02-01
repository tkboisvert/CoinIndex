using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TKBoisvert.CoinIndex
{
    /// <summary>
    /// Retrieves all .cl files in a designated directory and returns them as a jagged array.
    /// </summary>
    public class GetHashTableContents
    {
        private List<string> _FileName;
        private List<string> _FileContents;
        private string[][] _SplitArray;
        private string _Path;

        public GetHashTableContents(string pathFromUser)
        {
            _Path = pathFromUser;
            _FileName = new List<string>();
            _FileContents = new List<string>();
            GetFiles();
            _SplitArray = new string[_FileName.Count][];
        }

        public string[][] LoadFiles()
        {
            
            LoadArray();
            return _SplitArray;
        }

        private void LoadArray()
        {
            var x = 0;

            var tempFileContentsArray = _FileContents.ToArray();

            foreach (var name in _FileName)
            {
                _SplitArray[x] = new [] {name, tempFileContentsArray[x]};
                x++;
            }
        }

        private void GetFiles()
        {
            var d = new DirectoryInfo(_Path);

            foreach (var file in d.EnumerateFiles())
            {
                if (!file.Name.Contains(".ci")) continue;
                var actualName = string.Empty;

                actualName = file.Name.Where(c => c == '1' || c == '0')
                    .Aggregate(actualName, (current, c) => current + c);

                _FileName.Add(actualName);

                using (var sr = new StreamReader(file.FullName))
                {
                    _FileContents.Add(sr.ReadToEnd());
                }
            }
        }
    }
}

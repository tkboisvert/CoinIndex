using System;
using System.Drawing;

namespace TKBoisvert.CoinIndex.Imaging
{
    public class DateDataLine : IDisposable
    {

        private readonly Color colorBlack = Color.FromArgb(255, 0, 0, 0);

        private Bitmap _DateImage;

        public string _DateDataString;

        public DateDataLine(string dateImagePath)
        {
            _DateImage = new Bitmap(dateImagePath);
        }

        public void GetLineDateAvarage()
        {

            if (_DateDataString != "" || _DateDataString != string.Empty)
                _DateDataString = string.Empty;

            _DateDataString += "1";

            for (int x = 0; x < _DateImage.Width; x++)
            {
                if (_DateImage.GetPixel(x, _DateImage.Height / 2) == colorBlack 
                    && _DateDataString[_DateDataString.Length - 1] == '0')
                {
                    _DateDataString += 1;
                    
                }
                else if (_DateImage.GetPixel(x, _DateImage.Height / 2) != colorBlack
                    && _DateDataString[_DateDataString.Length - 1] == '1')
                {
                    _DateDataString += 0;
                }
            }
            
        }

        public void GetLineData()
        {
            if (_DateDataString != "" || _DateDataString != string.Empty)
                _DateDataString = string.Empty;
            for (int x = 0; x < _DateImage.Width; x++)
            {
                if(_DateImage.GetPixel(x, _DateImage.Height / 2) == colorBlack)
                {
                    _DateDataString += "1";
                }
                else
                {
                    _DateDataString += "0";
                }
            }
        }

        public void GetLineData(int line)
        {
            if (_DateDataString != "" || _DateDataString != string.Empty)
                _DateDataString = string.Empty;
            for (int x = 0; x < _DateImage.Width; x++)
            {
                if (_DateImage.GetPixel(x, line) == colorBlack)
                {
                    _DateDataString += "1";
                }
                else
                {
                    _DateDataString += "0";
                }
            }
        }

        public void Dispose()
        {
            _DateImage.Dispose();
        }

    }
}

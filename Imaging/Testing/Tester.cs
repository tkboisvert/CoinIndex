using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TKBoisvert.CoinIndex.Imaging.Tester
{
    public partial class Tester : Form
    {
        private FixBitmap _FixBmp;
        private List<string> _BitmapList; 
        private Bitmap lastImage;
        private int _ThresholdValue;

        public Tester()
        {
            InitializeComponent();
            //GetImageList();

            _ThresholdValue = 95;
            SetImage();

        }
        private void GetImageList()
        {
            _BitmapList = new List<string>();
            foreach (var directory in Directory.GetFiles(@"C:\USers\admin\desktop\programwork"))
            {
                _BitmapList.Add(directory);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetImage();
        }
        private void SetImage()
        {
            //listBox1.Items.Clear();

            //foreach (var bitmap in _BitmapList)
            do
            {

                if (lastImage != null)
                {
                    lastImage.Dispose();
                }

                _FixBmp = new FixBitmap(@"C:\USers\admin\desktop\programwork\source4.bmp");

                //if (textBox1.Text != "")
                //{
                //    _FixBmp._ThresholdValue = Convert.ToInt32(textBox1.Text);
                //}

                _FixBmp._ThresholdValue = _ThresholdValue;

                _FixBmp.Process();

                lastImage = new Bitmap(_FixBmp._DateImage);

                pictureBox1.Image = lastImage;

                label1.Text = _FixBmp._ThresholdValue.ToString();

                lastImage.Save(@"C:\users\Admin\desktop\Source4Date.bmp");

                var dDL = new DateDataLine(@"C:\users\Admin\desktop\Source4Date.bmp");

                dDL.GetLineData(4);

                listBox1.Items.Add(dDL._DateDataString + " TV " + _ThresholdValue + " 1");

                dDL.GetLineData(18);

                listBox1.Items.Add(dDL._DateDataString + " TV " + _ThresholdValue + " 2");

                dDL.GetLineData(28);

                listBox1.Items.Add(dDL._DateDataString + " TV " + _ThresholdValue + " 3");
                
                dDL.Dispose();
                _FixBmp.Dispose();
                _ThresholdValue += 1;
            } while (_ThresholdValue < 128);
        }
    }
}

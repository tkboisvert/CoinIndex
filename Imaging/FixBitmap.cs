using System;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging.Filters;

namespace TKBoisvert.CoinIndex.Imaging
{
    public class FixBitmap : IDisposable
    {
        private readonly Color colorBlack = Color.FromArgb(255, 0, 0, 0);

        public Bitmap _FullImageOfCoin;

        public Bitmap _DateImage;

        public Bitmap _Resized;

        public int datePointX;
        public int datePointY;
        public int datePointXLength;
        public int datePointYLength;

        public int _ThresholdValue { get; set; }

        public FixBitmap(string path)
        {
            _FullImageOfCoin = ScaleImage(new Bitmap(path), 2000, 2000);
            _ThresholdValue = 100; // Or Otsu()
            _Resized = _FullImageOfCoin;

        
        }

        public FixBitmap(Bitmap bmp)
        {
            _FullImageOfCoin = new Bitmap(bmp);
            _ThresholdValue = 100;
            _Resized = _FullImageOfCoin;
        }

        public void Process()
        {
            GrayscaleImage();
            
            ThresholdImage();

            CropOutEdges();

            _FullImageOfCoin.Save(@"C:\Users\Admin\Desktop\Cropped.bmp");

            SetDatePoints(.71/*make larger to go wider*/, .648/*make smaller to go higher*/, .64, .185);

            _DateImage = GetDateImage();
        }

        private void GrayscaleImage()
        {
            Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
            // apply the filter
            _FullImageOfCoin = filter.Apply(_FullImageOfCoin);
            
        }

        private void ThresholdImage()
        {
            Threshold threshold = new Threshold(_ThresholdValue);

            threshold.ApplyInPlace(_FullImageOfCoin);
        }

        public static Bitmap ScaleImage(Bitmap image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }



        public void SetDatePoints(double pointXPercent, double pointYPercent, double pointXLengthPercent, double pointYLengthPercent)
        {

            datePointX = Convert.ToInt32(_FullImageOfCoin.Width * pointXPercent);//make larger to go wider
            datePointY = Convert.ToInt32(_FullImageOfCoin.Height * pointYPercent);//make smaller to go higher

            datePointXLength = Convert.ToInt32((_FullImageOfCoin.Width - datePointX) * pointXLengthPercent);
            datePointYLength = Convert.ToInt32((_FullImageOfCoin.Height - datePointY) * pointYLengthPercent);

        }
        private Bitmap GetDateImage()
        {
            return _FullImageOfCoin.Clone(new Rectangle(datePointX, datePointY, datePointXLength, datePointYLength),
                                         _FullImageOfCoin.PixelFormat);
        }

        //Starts the crop process.
        private void CropOutEdges()
        {
            CropTopEdge();
            CropLeftEdge();
            CropBottomEdge();
            CropRightEdge();
        }

        //Looks for the y axis crop point
        private void CropTopEdge()
        {
            var loopBreaker = false;
            //...foreach y pixel...
            for (int y = 0; y < _FullImageOfCoin.Height - 6; y+=2)
            {
                //..foreach x pixel..
                for (int x = 0; x < _FullImageOfCoin.Width - 6; x+=2)
                {
                    //..if it's the right color...
                    if (_FullImageOfCoin.GetPixel(x, y) == colorBlack && _FullImageOfCoin.GetPixel(x + 3, y + 3) == colorBlack)
                    {
                        //..Crop and save it..
                        loopBreaker = true;
                        _FullImageOfCoin = _FullImageOfCoin.Clone(new Rectangle(0, y, _FullImageOfCoin.Width, _FullImageOfCoin.Height - y),
                                                                _FullImageOfCoin.PixelFormat);
                        break;
                    }
                }
                if (loopBreaker) { break; }
            }

        }

        //Looks for the x axis crop point
        private void CropLeftEdge()
        {
            var loopBreaker = false;
            //...foreach x pixel...
            for (int x = 0; x < _FullImageOfCoin.Width - 6; x+=2)
            {
                //..foreach y pixel...
                for (int y = 0; y < _FullImageOfCoin.Height - 6; y+=2)
                {
                    //..If the colors match..
                    if (_FullImageOfCoin.GetPixel(x, y) == colorBlack && _FullImageOfCoin.GetPixel(x + 3, y + 3) == colorBlack)
                    {
                        //..Crop and save.
                        loopBreaker = true;
                        _FullImageOfCoin = _FullImageOfCoin.Clone(new Rectangle(x, 0, _FullImageOfCoin.Width - x, _FullImageOfCoin.Height),
                                                                _FullImageOfCoin.PixelFormat);
                        break;
                    }
                }
                if (loopBreaker) { break; }
            }

        }


        private void CropBottomEdge()
        {
            var loopBreaker = false;

            for (int y = _FullImageOfCoin.Height - 6; y > 0; y-=2)
            {
                for (int x = _FullImageOfCoin.Width - 6; x > 0; x-=2)
                {
                    if (_FullImageOfCoin.GetPixel(x, y) == colorBlack && _FullImageOfCoin.GetPixel(x + 3, y + 3) == colorBlack)
                    {
                        //..Crop and save.
                        loopBreaker = true;
                        _FullImageOfCoin = _FullImageOfCoin.Clone(new Rectangle(0, 0, _FullImageOfCoin.Width, y),
                                                                _FullImageOfCoin.PixelFormat);
                        break;
                    }
                }
                if (loopBreaker) { break; }
            }
        }


        private void CropRightEdge()
        {
            var loopBreaker = false;

            for (int x = _FullImageOfCoin.Width - 6; x > 0; x-=2)
            {
                for (int y = _FullImageOfCoin.Height - 6; y > 0; y-=2)
                {
                    if (_FullImageOfCoin.GetPixel(x, y) == colorBlack && _FullImageOfCoin.GetPixel(x + 3, y + 3) == colorBlack)
                    {
                        //..Crop and save.
                        loopBreaker = true;
                        _FullImageOfCoin = _FullImageOfCoin.Clone(new Rectangle(0, 0, x, _FullImageOfCoin.Height),
                                                                _FullImageOfCoin.PixelFormat);
                        break;
                    }
                }
                if (loopBreaker) { break; }
            }
        }


        public int getOtsuThreshold(Bitmap bmp)
        {
            byte t = 0;
            float[] vet = new float[256];
            int[] hist = new int[256];
            vet.Initialize();

            float p1, p2, p12;
            int k;

            BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                                             ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* p = (byte*)(void*)bmData.Scan0.ToPointer();

                getHistogram(p, bmp.Width, bmp.Height, bmData.Stride, hist);


                for (k = 1; k != 255; k++)
                {
                    p1 = Px(0, k, hist);
                    p2 = Px(k + 1, 255, hist);
                    p12 = p1 * p2;
                    if (p12 == 0)
                        p12 = 1;
                    float diff = (Mx(0, k, hist) * p2) - (Mx(k + 1, 255, hist) * p1);
                    vet[k] = (float)diff * diff / p12;

                }
            }
            bmp.UnlockBits(bmData);

            t = (byte)findMax(vet, 256);

            return t;
        }

        private unsafe void getHistogram(byte* p, int w, int h, int ws, int[] hist)
        {
            hist.Initialize();
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w * 3; j += 3)
                {
                    int index = i * ws + j;
                    hist[p[index]]++;
                }
            }
        }

        private int findMax(float[] vec, int n)
        {
            float maxVec = 0;
            int idx = 0;
            int i;

            for (i = 1; i <= n - 1; i++)
            {
                if (vec[i] > maxVec)
                {
                    maxVec = vec[i];
                    idx = i;
                }
            }
            return idx;
        }

        private float Px(int init, int end, int[] hist)
        {
            int sum = 0;
            int i;
            for (i = init; i <= end; i++)
                sum += hist[i];

            return (float)sum;
        }

        // function is used to compute the mean values in the equation (mu)
        private float Mx(int init, int end, int[] hist)
        {
            int sum = 0;
            int i;
            for (i = init; i <= end; i++)
                sum += i * hist[i];

            return (float)sum;
        }

        public void Dispose()
        {
            if (_FullImageOfCoin != null && _DateImage != null)
            {
                _FullImageOfCoin.Dispose();
                _DateImage.Dispose();
            }
        }
    }
}

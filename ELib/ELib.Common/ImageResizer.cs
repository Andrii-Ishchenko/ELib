using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace ELib.Common
{
    public static class ImageResizer
    {
        static Color backgroundColor = Color.White;

        public static Image ResizeImage(Image source, int targetHeight, int targetWidth)
        {
            int sourceHeight = source.Height;
            int sourceWidth = source.Width;

            if (sourceHeight < targetHeight && sourceWidth < targetWidth)
                return source;

            double ratioX = (double)sourceWidth / targetWidth;
            double ratioY = (double)sourceHeight / targetHeight;

            double ratio = Math.Max(ratioX, ratioY);

            int newY = (int)(sourceHeight / ratio);
            int newX = (int)(sourceWidth / ratio);

            Bitmap newImage = new Bitmap(newX, newY);

            using(var g = Graphics.FromImage(newImage))
                g.DrawImage(source, 0, 0, newX, newY);

            return newImage;

        }

        public static Image CreateOutputImage(Image source, int targetHeight, int targetWidth)
        {
            source = ResizeImage(source, targetHeight, targetWidth);

            Bitmap newImage = new Bitmap(targetWidth, targetHeight);
            Graphics g = Graphics.FromImage(newImage);
            Brush b = new SolidBrush(backgroundColor);
            g.FillRectangle(b, new Rectangle() { Height = targetHeight, Width = targetWidth, X = 0, Y = 0 });

            
            int doubledDX = targetWidth - source.Width;
            int doubledDY = targetHeight - source.Height;
            int newX = doubledDX / 2;
            int newY = doubledDY / 2;

            g.DrawImage(source, newX, newY, source.Width, source.Height);
            return newImage;
            
        }


        public static void Test()
        {
            using (var image = Image.FromFile(@"d:\aspnet.png"))
            using (var newImage = ResizeImage(image, 200, 300))
            {
                newImage.Save(@"d:\test.png", ImageFormat.Png);
                Image small = CreateOutputImage(newImage, 300, 400);
                small.Save(@"d:\test-small.png", ImageFormat.Png);
            }
        }

    }
}

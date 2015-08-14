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

        public static void Test()
        {
            using (var image = Image.FromFile(@"d:\aspnet.png"))
            using (var newImage = ResizeImage(image, 300, 400))
            {
                newImage.Save(@"d:\test.png", ImageFormat.Png);
            }
        }

    }
}

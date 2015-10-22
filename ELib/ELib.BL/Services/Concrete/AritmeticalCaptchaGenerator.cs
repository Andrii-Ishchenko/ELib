using ELib.BL.Services.Abstract;
using System;
using System.Collections.Generic;
using ELib.BL.DtoEntities;
using System.Drawing;

namespace ELib.BL.Services.Concrete
{
    public class AritmeticalCaptchaGenerator : ICaptchaGenerator
    {
        List<string> fonts = new List<string>();

        public AritmeticalCaptchaGenerator()
        {
            fonts.AddRange(new[] { "Tahoma", "Segoe UI", "Arial", "Comic Sans", "Times New Roman" });
        }

        public CaptchaDto Generate(int width, int height)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            
            //generate new question
            int a = rand.Next(10, 99);
            int b = rand.Next(0, 9);
            var question = string.Format("{0} + {1} = ?", a, b);

            //answer
            string answer = (a + b).ToString();

            Image image = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(image))
            {

                g.FillRectangle(Brushes.White, new Rectangle(0, 0, image.Width, image.Height));

                //add noise
                int i, r, x, y;
                var pen = new Pen(Color.Yellow);
                for (i = 1; i < 20; i++)
                {
                    pen.Color = Color.FromArgb(
                    (rand.Next(0, 255)),
                    (rand.Next(0, 255)),
                    (rand.Next(0, 255)));

                    r = rand.Next(0, (width / 3));
                    x = rand.Next(0, width);
                    y = rand.Next(0, height);

                    g.DrawEllipse(pen, x - r, y - r, r, r);
                }


                //add question
                int fontIndex = rand.Next(fonts.Count);
                g.DrawString(question, new Font(fonts[fontIndex], width/7), Brushes.Gray, 2, height / 3);

            }
            CaptchaDto captcha = new CaptchaDto();
            captcha.Image = image;
            captcha.Password = answer;
            return captcha;

        }
    }
}

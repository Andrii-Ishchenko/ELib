using ELib.BL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELib.BL.DtoEntities;
using System.Drawing;

namespace ELib.BL.Services.Concrete
{
    public class OrdinaryCaptchaGenerator : ICaptchaGenerator
    {
        readonly double pointsperpixel = 0.7528125;

        List<Brush> fontColors = new List<Brush>();
        List<Brush> featureColors = new List<Brush>();
        List<string> fonts = new List<string>();


        public OrdinaryCaptchaGenerator()
        {
            fontColors.AddRange(new[] { Brushes.Black, Brushes.Gray, Brushes.DarkGray,Brushes.SteelBlue,Brushes.Plum, Brushes.DarkGreen, Brushes.DarkRed });
            featureColors.AddRange(new[] { Brushes.Gray, Brushes.LightGray, Brushes.LightBlue, Brushes.Navy, Brushes.DarkRed, Brushes.Tomato,Brushes.Thistle });
            fonts.AddRange(new[] {"Verdana","Tahoma","Segoe UI", "Arial","Comic Sans","Times New Roman" });
        }
        
        public CaptchaDto Generate(int width, int height)
        {
            string chars = "0123456789qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
            //my implementaion here
            Random r = new Random();
            int length = 3;
            double v = r.NextDouble();
            if (v > 0.33) length = 4;
            if (v > 0.66) length = 5;

            StringBuilder tokenBuilder = new StringBuilder();

            for (int i=0;i< length; i++)
            {
                int index = r.Next(chars.Length);
                tokenBuilder.Append(chars[index]);
            }

            String token = tokenBuilder.ToString();


            Image image = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(image))
            {
                int lettersZoneHeight = Convert.ToInt32( Math.Round(0.5 * height) );

                int fontsize = Convert.ToInt32(Math.Round(lettersZoneHeight*pointsperpixel));

                int dx = 10;
                int dy = 10;

                int stepx = Convert.ToInt32(Math.Round((double)width/(2*length+1)));

                int fontIndex = 0;
                int fontColorIndex = 0;

                g.FillRectangle(Brushes.White, 0, 0, width, height);

                Point start, finish;
                for(int i = 0; i < 10; i++)
                {
                    start = new Point(r.Next(width), r.Next(height));
                    finish = new Point(r.Next(width), r.Next(height));
                    fontColorIndex = r.Next(featureColors.Count);
                    g.DrawLine(new Pen(featureColors[fontColorIndex]), start, finish);
                }

                for(int i=0; i< Convert.ToInt32(0.12 * width * height); i++)
                {
                    fontColorIndex = r.Next(featureColors.Count);
                    g.FillRectangle(featureColors[fontColorIndex],r.Next(width), r.Next(height),1,1);
                }

                for(int i=0;i< length; i++)
                {
                    fontIndex = r.Next(fonts.Count);
                    fontColorIndex = r.Next(fontColors.Count);
                    g.DrawString(token[i].ToString(),new Font(fonts[fontIndex],fontsize),fontColors[fontColorIndex],stepx*(i+1)-dx+2*r.Next(dx),Convert.ToInt32( (height-lettersZoneHeight)/2 ) - dy + 2 * r.Next(dy));
                }

               
            }

            CaptchaDto captcha = new CaptchaDto();
            captcha.Image = image;
            captcha.Password = token;
                return captcha;
        }
    }
}

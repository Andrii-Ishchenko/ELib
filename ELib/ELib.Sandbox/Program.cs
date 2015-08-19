using ELib.BL.DtoEntities;
using ELib.BL.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ELib.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            OrdinaryCaptchaGenerator generator = new OrdinaryCaptchaGenerator();
            CaptchaDto captcha;

            for (int i = 0; i < 25; i++)
            {
                captcha = generator.Generate();
                captcha.Image.Save(String.Format(@"D:\captchas\{0}.png",i), ImageFormat.Png);
                Thread.Sleep(50);
            }
        }
    }
}

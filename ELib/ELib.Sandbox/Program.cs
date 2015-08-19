﻿using ELib.BL.DtoEntities;
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
            OrdinaryCaptchaGenerator generatorOrdinary = new OrdinaryCaptchaGenerator();
            AritmeticalCaptchaGenerator generatorAritmetical = new AritmeticalCaptchaGenerator();

            CaptchaService capchaService = new CaptchaService();
            capchaService.AddGenerator(generatorOrdinary);
            capchaService.AddGenerator(generatorAritmetical);

            CaptchaDto captcha;

            for (int i = 0; i < 25; i++)
            {
                captcha = capchaService.GenerateCaptcha();
                captcha.Image.Save(String.Format(@"C:\captchas\{0}.png",i), ImageFormat.Png);
                Thread.Sleep(50);
            }
        }
    }
}

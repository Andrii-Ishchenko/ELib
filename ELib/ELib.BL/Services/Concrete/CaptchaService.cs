using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;

namespace ELib.BL.Services.Concrete
{
    public class CaptchaService : ICaptchaService
    {
        List<ICaptchaGenerator> generators = new List<ICaptchaGenerator>();

        public void AddGenerator(ICaptchaGenerator cg)
        {
            generators.Add(cg);
        }

        public CaptchaDto GenerateCaptcha(int width, int height)
        {
            int count = generators.Count();

            if (count == 0)
                throw new Exception("Can't create Captcha. No Captcha Generator has been given.");

            Random r = new Random();
            int selected = r.Next(count);

            return generators[selected].Generate(width,height);
        }
    }

}

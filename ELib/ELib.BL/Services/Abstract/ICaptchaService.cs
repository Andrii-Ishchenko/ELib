using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELib.BL.DtoEntities;
namespace ELib.BL.Services.Abstract
{
    public interface ICaptchaService 
    {
        void AddGenerator(ICaptchaGenerator cg);
        CaptchaDto GenerateCaptcha(int width, int height);
    }
}

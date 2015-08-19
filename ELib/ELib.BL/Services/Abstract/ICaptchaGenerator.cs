using ELib.BL.DtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.Services.Abstract
{
    public interface ICaptchaGenerator
    {
        CaptchaDto Generate(int width,int height);
    }
}

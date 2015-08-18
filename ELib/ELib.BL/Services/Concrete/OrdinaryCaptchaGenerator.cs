using ELib.BL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELib.BL.DtoEntities;

namespace ELib.BL.Services.Concrete
{
    public class OrdinaryCaptchaGenerator : ICaptchaGenerator
    {
        public CaptchaDto Generate()
        {
            //my implementaion here
            return new CaptchaDto();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ELib.BL.DtoEntities
{
    public class CaptchaDto
    {
        public int Id;

        public string Password;

        public string ImageHash;

        public Image Image;

    }
}

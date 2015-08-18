using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.Services.Abstract
{
    public interface IFileService
    {
        bool SaveProfileImage(byte[] file, string fileName);
        bool SaveBookImage(byte[] file, string fileName);
        bool SaveBookFile(byte[] file, string fileName);
    }
}

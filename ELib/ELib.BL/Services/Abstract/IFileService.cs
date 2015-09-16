using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.Services.Abstract
{
    public interface IFileService
    {
        bool SaveProfileImage(byte[] file, string fileName, int userId);
        bool SaveBookImage(byte[] file, string fileName, int bookId, int userId);
        bool SaveAuthorImage(byte[] file, string fileName, int bookId, int userId);
        bool SaveBookFile(byte[] file, string fileName, int bookInstanceId, int userId);
        String GetBookImagePath(String hash);
        string GetBookFilePath(string hash);
        string GetBookFileNameByHash(string hash);

        byte[] GetBookImage(String hash, int w, int h);
        byte[] GetProfileImage(String hash, int w, int h);
        byte[] GetAuthorImage(String hash, int w, int h);

    }
}

using System;
using System.IO;

namespace ELib.BL.Services.Abstract
{
    public interface IFileService
    {
        bool SaveProfileImage(byte[] file, string fileName, int userId);
        bool SaveBookImage(byte[] file, string fileName, int bookId, int userId);
        bool SaveAuthorImage(byte[] file, string fileName, int bookId, int userId);
        bool SaveBookFile(byte[] file, string fileName, int bookInstanceId, int userId);

        string GetBookFileNameByHash(string hash);
        FileStream GetBookFile(string hash);

        byte[] GetBookImage(String hash, int width, int height);
        byte[] GetProfileImage(String hash, int width, int height);
        byte[] GetAuthorImage(String hash, int width, int height);
    }
}

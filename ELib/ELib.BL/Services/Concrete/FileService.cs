using ELib.BL.Services.Abstract;
using ELib.DAL.Infrastructure.Abstract;
using System;
using System.IO;
using System.Security.Cryptography;

namespace ELib.BL.Services.Concrete
{
    public class FileService : IFileService
    {
        protected readonly IUnitOfWorkFactory _factory;

        public FileService(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

        public void SaveImage(byte[] file)
        {
            string fileHash = getFileHash(file);

            string filePath = String.Format(@"D:\LibraryContent\Images\{0}", fileHash);

            File.WriteAllBytes(filePath, file);
        }

        private string getFileHash(byte[] file)
        {
            SHA1Managed sha = new SHA1Managed();
            byte[] hash = sha.ComputeHash(file);
            string stringHash = BitConverter.ToString(hash).Replace("-", string.Empty);
    
            return stringHash;
        }
    }
}

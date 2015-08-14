using ELib.BL.Services.Abstract;
using ELib.DAL.Infrastructure.Abstract;
using System;
using System.IO;
using System.Security.Cryptography;

namespace ELib.BL.Services.Concrete
{
    public class FileService : IFileService
    {
        const int DIRECTORY_NAME_LENGTH = 2;
        const string IMAGES_FOLDER_PATH = @"D:\LibraryContent\Images";

        protected readonly IUnitOfWorkFactory _factory;

        public FileService(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

        public void SaveImage(byte[] file)
        {
            string fileHash = getFileHash(file);

            createDirectoriesIfNoExist(fileHash);

            string directoryPath = createDirectoriesIfNoExist(fileHash);
            
            string filePath = String.Format(@"{0}\{1}",directoryPath,fileHash);

            File.WriteAllBytes(filePath, file);
        }

        private string getFileHash(byte[] file)
        {
            SHA1Managed sha = new SHA1Managed();
            byte[] hash = sha.ComputeHash(file);
            string stringHash = BitConverter.ToString(hash).Replace("-", string.Empty);
            
            return stringHash;
        }

        private string createDirectoriesIfNoExist(string fileHash)
        {
            string directoryPath = String.Format(@"{0}\{1}\{2}", 
                IMAGES_FOLDER_PATH, fileHash.Substring(0, DIRECTORY_NAME_LENGTH), fileHash.Substring(DIRECTORY_NAME_LENGTH, DIRECTORY_NAME_LENGTH));

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        
            return directoryPath;
        }
    }
}

using ELib.BL.Enums;
using ELib.BL.Services.Abstract;
using ELib.DAL.Infrastructure.Abstract;
using System;
using System.IO;
using System.Security.Cryptography;

namespace ELib.BL.Services.Concrete
{
    public class FileService : IFileService
    {
        private readonly int DIRECTORY_NAME_LENGTH = 2;
        private readonly string PROFILE_IMAGES_FOLDER_PATH = @"D:\LibraryContent\ProfileImages";
        private readonly string BOOK_IMAGES_FOLDER_PATH = @"D:\LibraryContent\BookImages";
        private readonly string BOOK_FILES_FOLDER_PATH = @"D:\LibraryContent\BookFiles";

        private readonly IUnitOfWorkFactory _factory;

        public FileService(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

        public bool SaveProfileImage(byte[] file, string fileName)
        {
            string extension = getExtension(fileName);

            if (validateExtension(Enum.GetNames(typeof(ProfileImageExtensions)), extension))
            {
                saveFile(file, extension, PROFILE_IMAGES_FOLDER_PATH);

                return true;
            }

            return false;
        }

        public bool SaveBookImage(byte[] file, string fileName)
        {
            string extension = getExtension(fileName);

            if (validateExtension(Enum.GetNames(typeof(ProfileImageExtensions)), extension))
            {
                saveFile(file, extension, BOOK_IMAGES_FOLDER_PATH);

                return true;
            }

            return false;
        }

        public bool SaveBookFile(byte[] file, string fileName)
        {
            string extension = getExtension(fileName);

            if (validateExtension(Enum.GetNames(typeof(ProfileImageExtensions)), extension))
            {
                saveFile(file, extension, BOOK_FILES_FOLDER_PATH);

                return true;
            }

            return false;
        }

        private string getFileHash(byte[] file)
        {
            SHA1Managed sha = new SHA1Managed();
            byte[] hash = sha.ComputeHash(file);
            string stringHash = BitConverter.ToString(hash).Replace("-", string.Empty);
            
            return stringHash;
        }

        private string createDirectoriesIfNoExist(string fileHash, string rootDirectoryPath)
        {
            string directoryPath = String.Format(@"{0}\{1}\{2}", 
                rootDirectoryPath, fileHash.Substring(0, DIRECTORY_NAME_LENGTH), fileHash.Substring(DIRECTORY_NAME_LENGTH, DIRECTORY_NAME_LENGTH));

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        
            return directoryPath;
        }

        private void saveFile(byte[] file, string extension, string rootDirectoryPath)
        {
            string fileHash = getFileHash(file);

            createDirectoriesIfNoExist(fileHash, rootDirectoryPath);

            string directoryPath = createDirectoriesIfNoExist(fileHash, rootDirectoryPath);

            string filePath = String.Format(@"{0}\{1}.{2}", directoryPath, fileHash, extension);

            File.WriteAllBytes(filePath, file);
        }

        private string getExtension(string fileName)
        {
            return Path.GetExtension(fileName).Replace(".", String.Empty).ToUpper();
        }

        private bool validateExtension(string[] extensions, string extensionToValidate)
        {
            int position = Array.IndexOf(extensions, extensionToValidate);

            return position > -1;
        }
    }
}

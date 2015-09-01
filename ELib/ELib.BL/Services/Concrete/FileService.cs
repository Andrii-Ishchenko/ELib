using ELib.BL.Enums;
using ELib.BL.Services.Abstract;
using ELib.Common;
using ELib.DAL.Infrastructure.Abstract;
using ELib.Domain.Entities;
using System;
using System.Linq;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;

namespace ELib.BL.Services.Concrete
{
    public class FileService : IFileService
    {
        private readonly int DIRECTORY_NAME_LENGTH = Convert.ToInt32(ConfigurationManager.AppSettings["DirectoryNameLength"]);
        private readonly string PROFILE_IMAGES_FOLDER_PATH = ConfigurationManager.AppSettings["ProfileImagesFolderPath"];
        private readonly string BOOK_IMAGES_FOLDER_PATH = ConfigurationManager.AppSettings["BookImagesFolderPath"];
        private readonly string BOOK_FILES_FOLDER_PATH = ConfigurationManager.AppSettings["BookFilesFolderPath"];
        private readonly int MAX_PROFILE_IMAGE_SIZE = Int32.Parse(ConfigurationManager.AppSettings["MaxProfileImageSize"]); //bytes
        private readonly int MAX_BOOK_IMAGE_SIZE = Int32.Parse(ConfigurationManager.AppSettings["MaxBookImageSize"]); // bytes
        private readonly int MAX_BOOK_FILE_SIZE = Int32.Parse(ConfigurationManager.AppSettings["MaxBookFileSize"]); //bytes

        private readonly IUnitOfWorkFactory _factory;
        private ELogger logger;

        public FileService(IUnitOfWorkFactory factory)
        {
            _factory = factory;
            logger = ELoggerFactory.GetInstance().GetLogger(GetType().FullName);
        }

        public bool SaveProfileImage(byte[] file, string fileName, int userId)
        {
            string extension = getExtension(fileName);
            var p  = ConfigurationManager.AppSettings["ProfileImagesFolderPath"];
            if (validateFile(file, extension, userId, typeof(ProfileImageExtensions), MAX_PROFILE_IMAGE_SIZE))
            {
                string fileHash = saveFile(file, extension, PROFILE_IMAGES_FOLDER_PATH);

                using (IUnitOfWork uow = _factory.Create())
                {
                    var person = uow.Repository<Person>().GetById(userId);
                    person.ImageHash = fileHash;
                    uow.Repository<Person>().Update(person);
                    uow.Save();
                }

                return true;
            }

            return false;
        }

        public bool SaveBookImage(byte[] file, string fileName, int bookId, int userId)
        {
            string extension = getExtension(fileName);

            if (validateFile(file, extension, userId, typeof(BookImageExtensions), MAX_BOOK_IMAGE_SIZE))
            {
                string fileHash = saveFile(file, extension, BOOK_IMAGES_FOLDER_PATH);

                using (IUnitOfWork uow = _factory.Create())
                {
                    var book = uow.Repository<Book>().GetById(bookId);
                    book.ImageHash = fileHash;
                    uow.Repository<Book>().Update(book);
                    uow.Save();
                }

                return true;
            }

            return false;
        }

        public bool SaveBookFile(byte[] file, string fileName, int bookId, int userId)
        {
            string extension = getExtension(fileName);

            if (validateFile(file, extension, userId, typeof(BookFileExtensions), MAX_BOOK_FILE_SIZE))
            {
                string fileHash = saveFile(file, extension, BOOK_FILES_FOLDER_PATH);

                using (IUnitOfWork uow = _factory.Create())
                {
                    var book = uow.Repository<Book>().GetById(bookId);

                    if (book == null)
                        return false;

                    var bi = book.BookInstances.FirstOrDefault(b => b.FileHash == fileHash);

                    if (bi== null)
                    {
                        bi = new BookInstance();
                        bi.BookId = book.Id;
                        bi.InsertDate = DateTime.Now;
                        bi.FileName = fileName;
                        bi.FileHash = fileHash;
                        uow.Repository<BookInstance>().Insert(bi);
                        uow.Save();
                    }                   
                }

                return true;
            }

            return false;
        }

        public String GetBookImagePath(string fileHash)
        {
            string directoryPath = DirectoryPath(fileHash, "");
            string fullPath  = String.Format(@"{0}\{1}.png", directoryPath, fileHash);
            logger.Info(String.Format("Getting BookImage, hash:{0} ,path:{1} ",fileHash,fullPath));

            return fullPath;
        }

        private bool validateFile(byte[] file, string extension, int userId, Type fileType,  int maxFileSize)
        {
            bool isValidExtension = validateExtension(Enum.GetNames(fileType), extension);

            if (isValidExtension)
            {
                if(validateFileSize(file, maxFileSize))
                {
                    return true;
                }

                logger.Warn(String.Format("Invalid file size, UserId: {0}", userId));
                return false;
            }
                      
            logger.Warn(String.Format("Invalid file extension, UserId: {0}", userId));
            return false;
        }

        private bool validateFileSize(byte[] file, int maxFileSize)
        {
            return file.Length <= maxFileSize;
        } 

        private string getFileHash(byte[] file)
        {
            SHA1Managed sha = new SHA1Managed();
            byte[] hash = sha.ComputeHash(file);
            string stringHash = BitConverter.ToString(hash).Replace("-", string.Empty);
            
            return stringHash;
        }


        private string DirectoryPath(string fileHash, string rootDirectoryPath)
        {
            if (rootDirectoryPath == "")
                return String.Format(@"{1}\{2}",
                         fileHash.Substring(0, DIRECTORY_NAME_LENGTH),
                         fileHash.Substring(DIRECTORY_NAME_LENGTH, DIRECTORY_NAME_LENGTH));

            return  String.Format(@"{0}\{1}\{2}",
                        rootDirectoryPath, 
                        fileHash.Substring(0, DIRECTORY_NAME_LENGTH), 
                        fileHash.Substring(DIRECTORY_NAME_LENGTH, DIRECTORY_NAME_LENGTH));
        }

        private string createDirectoriesIfNoExist(string fileHash, string rootDirectoryPath)
        {
            string directoryPath = DirectoryPath(fileHash, rootDirectoryPath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        
            return directoryPath;
        }

        private string saveFile(byte[] file, string extension, string rootDirectoryPath)
        {
            string fileHash = getFileHash(file);

            string directoryPath = createDirectoriesIfNoExist(fileHash, rootDirectoryPath);

            string filePath = String.Format(@"{0}\{1}.{2}", directoryPath, fileHash, extension);

            File.WriteAllBytes(filePath, file);

            return fileHash;
        }

        private string getExtension(string fileName)
        {
            return Path.GetExtension(fileName.Trim('\"')).Replace(".", String.Empty).ToUpper();
        }

        private bool validateExtension(string[] extensions, string extensionToValidate)
        {
            int position = Array.IndexOf(extensions, extensionToValidate);

            return position > -1;
        }

        
    }
}

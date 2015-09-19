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
using System.Drawing;
using System.Drawing.Imaging;
using ELib.BL.DtoEntities;

namespace ELib.BL.Services.Concrete
{
    public class FileService : IFileService
    {
        #region AppSettings 
        private readonly int DIRECTORY_NAME_LENGTH = Convert.ToInt32(ConfigurationManager.AppSettings["DirectoryNameLength"]);
        private readonly string PROFILE_IMAGES_FOLDER_PATH = ConfigurationManager.AppSettings["ProfileImagesFolderPath"];
        private readonly string BOOK_IMAGES_FOLDER_PATH = ConfigurationManager.AppSettings["BookImagesFolderPath"];
        private readonly string BOOK_FILES_FOLDER_PATH = ConfigurationManager.AppSettings["BookFilesFolderPath"];
        private readonly string AUTHOR_IMAGES_FOLDER_PATH = ConfigurationManager.AppSettings["AuthorImagesFolderPath"];
        private readonly int MAX_PROFILE_IMAGE_SIZE = Int32.Parse(ConfigurationManager.AppSettings["MaxProfileImageSize"]); //bytes
        private readonly int MAX_AUTHOR_IMAGE_SIZE = Int32.Parse(ConfigurationManager.AppSettings["MaxAuthorImageSize"]);
        private readonly int MAX_BOOK_IMAGE_SIZE = Int32.Parse(ConfigurationManager.AppSettings["MaxBookImageSize"]); // bytes
        private readonly int MAX_BOOK_FILE_SIZE = Int32.Parse(ConfigurationManager.AppSettings["MaxBookFileSize"]); //bytes
        private readonly string PREDEFINED_IMAGE_EXTENSION = ImageExtensions.PNG.ToString().ToLowerInvariant();
        #endregion AppSettings

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
            
            if (validateFile(file, extension, userId, typeof(ImageExtensions), MAX_PROFILE_IMAGE_SIZE))
            {
                string fileHash = saveImage(file, extension, PROFILE_IMAGES_FOLDER_PATH);

                using (IUnitOfWork uow = _factory.Create())
                {
                    var person = uow.Repository<Person>().GetById(userId);
                    string oldHash = person.ImageHash;
                    person.ImageHash = fileHash;
                    uow.Repository<Person>().Update(person);
                    uow.Save();
                    RemoveUnusedProfileImage(oldHash,uow);
                    uow.Save();
                }

                return true;
            }

            return false;
        }

        public bool SaveBookImage(byte[] file, string fileName, int bookId, int userId)
        {
            string extension = getExtension(fileName);

            if (validateFile(file, extension, userId, typeof(ImageExtensions), MAX_BOOK_IMAGE_SIZE))
            {
                string fileHash = saveImage(file, extension, BOOK_IMAGES_FOLDER_PATH);

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

        public bool SaveAuthorImage(byte[] file, string fileName, int authorId, int userId)
        {
           
            string extension = getExtension(fileName);

            if (validateFile(file, extension, userId, typeof(ImageExtensions), MAX_AUTHOR_IMAGE_SIZE))
            {
                string fileHash = saveImage(file, extension, AUTHOR_IMAGES_FOLDER_PATH);

                using (IUnitOfWork uow = _factory.Create())
                {
                    var author = uow.Repository<Author>().GetById(authorId);
                    author.ImageHash = fileHash;
                    uow.Repository<Author>().Update(author);
                    uow.Save();
                }

                return true;
            }

            return false;
        }
        public void RemoveUnusedProfileImage(string fileHash, IUnitOfWork uow)
        {
                int cnt = uow.Repository<Person>().Get(p=>p.ImageHash==fileHash).Count();
                if (cnt == 0)
                {
                    String path = directoryPath(fileHash, PROFILE_IMAGES_FOLDER_PATH);
                    String filePath = String.Format(@"{0}\{1}.{2}", path, fileHash, PREDEFINED_IMAGE_EXTENSION);
                    File.Delete(filePath);
                }
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


        public byte[] GetBookImage(string hash, int w, int h)
        {
            using (IUnitOfWork uow = _factory.Create())
            {
                var book = uow.Repository<Book>().Get(b => b.ImageHash == hash).FirstOrDefault();
                if (book == null)
                    return null;

                //ENSURE THAT FILEPATH IS CORRECT
                String fileName = getFilePath(hash, BOOK_IMAGES_FOLDER_PATH);
                Image i = Image.FromFile(fileName);
                i = ImageResizer.ResizeImage(i,h,w);
                
                using (MemoryStream ms = new MemoryStream())
                {
                    i.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
               
            }
        }

        public byte[] GetProfileImage(string hash, int w, int h)
        {
            using(IUnitOfWork uow = _factory.Create())
            {
                var profile = uow.Repository<Person>().Get(p => p.ImageHash == hash).FirstOrDefault();
                if (profile == null)
                    return null;
                String fileName = getFilePath(hash, PROFILE_IMAGES_FOLDER_PATH);
                Image i = Image.FromFile(fileName);
                i = ImageResizer.ResizeImage(i,h,w);
                
               
                using (MemoryStream ms = new MemoryStream())
                {
                    i.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }           
            }
        }

        public byte[] GetAuthorImage(string hash, int w, int h)
        {
            using (IUnitOfWork uow = _factory.Create())
            {
                var author = uow.Repository<Author>().Get(b => b.ImageHash == hash).FirstOrDefault();
                if (author == null)
                    return null;

                //ENSURE THAT FILEPATH IS CORRECT
                String fileName = getFilePath(hash, AUTHOR_IMAGES_FOLDER_PATH);
                Image i = Image.FromFile(fileName);
                i = ImageResizer.ResizeImage(i, h, w);

                using (MemoryStream ms = new MemoryStream())
                {
                    i.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }

            }
        }
        

        public string GetBookFileName(string hash)
        {
            using (var uow = _factory.Create())
            {
                return uow.Repository<BookInstance>().Get(x => x.FileHash == hash).FirstOrDefault().FileName;
            }
        }

        public FileStream GetBookFile(string hash)
        {
            string filePath = getFilePath(hash, BOOK_FILES_FOLDER_PATH);

            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            var bookInstance = getBookInstanceByHash(hash);
            increaseBookDownloadCounter(bookInstance);

            return fs;
        }

        #region PrivateMethods
        private void increaseBookDownloadCounter(BookInstance bookInstance)
        {
            using (var uow = _factory.Create())
            {
                bookInstance.DownloadCount++;
                uow.Repository<BookInstance>().Update(bookInstance);
                var book = uow.Repository<Book>().GetById(bookInstance.BookId);
                book.TotalDownloadCount++;
                uow.Repository<Book>().Update(book);
                uow.Save();
            }
        }

        private BookInstance getBookInstanceByHash(string hash)
        {
            BookInstance bookInstance;

            using (var uow = _factory.Create())
            {
                 bookInstance = uow.Repository<BookInstance>().Get(x => x.FileHash == hash).FirstOrDefault();
            }

            if (bookInstance == null)
                throw new ArgumentException();

            return bookInstance;
        }


        private string getFilePath(string fileHash, string rootDirectory)
        {
            string directoryPath = this.directoryPath(fileHash, rootDirectory);

            string fullPath = Directory.GetFiles(directoryPath, String.Format("{0}.*", fileHash)).First();
            logger.Info(String.Format("Getting File, hash:{0} ,path:{1} ", fileHash, fullPath));

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

        private string directoryPath(string fileHash, string rootDirectoryPath)
        {
           return  String.Format(@"{0}\{1}\{2}",
                        rootDirectoryPath, 
                        fileHash.Substring(0, DIRECTORY_NAME_LENGTH), 
                        fileHash.Substring(DIRECTORY_NAME_LENGTH, DIRECTORY_NAME_LENGTH));
        }

        private string createDirectoriesIfNoExist(string fileHash, string rootDirectoryPath)
        {
            string directoryPath = this.directoryPath(fileHash, rootDirectoryPath);

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

        private string saveImage(byte[] file, string extension,string rootDirectoryPath)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Image i = Image.FromStream(new MemoryStream(file)))
                {
                    i.Save(ms, ImageFormat.Png);
                    byte[] b = ms.ToArray();

                    string fileHash = getFileHash(b);
                    string directoryPath = createDirectoriesIfNoExist(fileHash, rootDirectoryPath);
                    string filePath = String.Format(@"{0}\{1}.{2}", directoryPath, fileHash, PREDEFINED_IMAGE_EXTENSION);
                    File.WriteAllBytes(filePath, b);
                    return fileHash;
                }
            }
        }

        private string getExtension(string fileName)
        {

            return Path.GetExtension(fileName.Replace("\"",String.Empty)).Replace(".", String.Empty).ToUpperInvariant();
        }

        private bool validateExtension(string[] extensions, string extensionToValidate)
        {
            int position = Array.IndexOf(extensions, extensionToValidate);

            return position > -1;
        }
    
        #endregion PrivateMethods
    }
}

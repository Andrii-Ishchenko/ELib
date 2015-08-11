using ELib.BL.Services.Abstract;
using ELib.Domain.Entities;
using ELib.DAL.Infrastructure.Abstract;
using ELib.BL.Mapper;
using ELib.BL.DtoEntities;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ELib.BL.Services.Concrete
{
    public class PersonService : BaseService<Person, PersonDto>, IPersonService
    {
        public PersonService(IUnitOfWorkFactory factory)
            : base(factory)
        {
        }

        public bool Validate(string login, string password)
        {
            using (var uow = _factory.Create())
            {
                return uow.Repository<Person>().Get(u => u.Login == login && u.Password == password).Any();
            }
        }

        public PersonDto GetByLogin(string login)
        {
            using (var uow = _factory.Create())
            {
                var entity = uow.Repository<Person>().Get(log => log.Login == login).FirstOrDefault();
                var entityDto = AutoMapper.Mapper.Map<PersonDto>(entity);
                return entityDto;
            }
        }
        public void UpdateLastActivity(string login)
        {

            using (var uow = _factory.Create())
            {
                var userdb = uow.Repository<Person>().Get(l => l.Login == login).FirstOrDefault();
                if (userdb == null) return;
                userdb.LastActivationDate = DateTime.Now;
                uow.Repository<Person>().Update(userdb);
                uow.Save();
            }

        }
        public bool ChangePassword(string login, string pwd, string newpwd, bool allownullforpwd)
        {
            using (var uow = _factory.Create())
            {
                var userdb = uow.Repository<Person>().Get(user => user.Login == login).FirstOrDefault();
                if (userdb == null) return false;
                if (userdb.Password == pwd || (allownullforpwd && pwd == null))
                {
                    userdb.Password = newpwd;
                    uow.Repository<Person>().Update(userdb);
                    uow.Save();
                    return true;
                }
                return false;
            }

        }

        public string EncodePassword(string psw)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] checkSum = md5.ComputeHash(Encoding.UTF8.GetBytes(psw));
            string result = BitConverter.ToString(checkSum).Replace("-", String.Empty);
            return result;
        }
        public bool CheckPassword(string password)
        {
            Regex R = new Regex("^[0-9]{1,6}$");
            Match M = R.Match(password);
            return M.Success;
        }

        public string CreatePassword()
        {
            Random rnd = new Random();
            const string rc = "qwertyuiopasdfghjklzxcvbnm0123456789";
            char[] pwd = new char[7];
            for (int i = 0; i < pwd.Length; i++)
                pwd[i] = rc[rnd.Next(rc.Length)];
            return new string(pwd);
        }

        public string ResetPassword(string login)
        {
            string newpwd = CreatePassword();
            if (ChangePassword(login, null, EncodePassword(newpwd), true))
                return newpwd;
            return null;
        }
        public bool ExistLogin(string login)
        {
            using (var uow = _factory.Create())
            {
                return uow.Repository<Person>().Get(u => u.Login == login).Any();
            }
        }

        public bool ExistEmail(string email)
        {
            using (var uow = _factory.Create())
            {
                return uow.Repository<Person>().Get(u => u.Email == email).Any();
            }
        }

       

    }
}

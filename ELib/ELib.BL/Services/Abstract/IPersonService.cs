using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.Services.Abstract
{
    public interface IPersonService : IBaseService<Person, PersonDto>
    {
        bool Validate(string login, string password);

        PersonDto GetByLogin(string login);

        void UpdateLastActivity(string login);

        bool ChangePassword(string login, string pwd, string newpwd, bool allownullforpwd);

        string EncodePassword(string psw);

        bool CheckPassword(string password);

        string CreatePassword();

        string ResetPassword(string login);

        bool ExistLogin(string login);

        bool ExistEmail(string email);

    }
}

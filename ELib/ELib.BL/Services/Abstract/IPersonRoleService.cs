using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.Services.Abstract
{
    public interface IPersonRoleService : IBaseService<PersonRole, PersonRoleDto>
    {
        string ReadRoleForUser(string login);

        bool Exists(string rolename);

        IEnumerable<string> ReadUsersInRole(string rolename);

        bool IsUserInRole(string login, string rolename);

        void AddUsersToRole(IEnumerable<string> users, string role);

        PersonRoleDto GetByName(string roleName);
    }
}

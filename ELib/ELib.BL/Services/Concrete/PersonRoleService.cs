using ELib.BL.Services.Abstract;
using ELib.Domain.Entities;
using ELib.DAL.Infrastructure.Abstract;
using ELib.BL.Mapper;
using ELib.BL.DtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.Services.Concrete
{
    public class PersonRoleService : BaseService<PersonRole, PersonRoleDto>, IPersonRoleService
    {
        public PersonRoleService(IUnitOfWorkFactory factory)
            : base(factory)
        {

        }

        public string ReadRoleForUser(string login)
        {
            using (var uow = _factory.Create())
            {
                return uow.Repository<Person>().Get(x => x.Login == login).FirstOrDefault().PersonRole.Name;
            }
        }

        public bool Exists(string rolename)
        {
            using (var uow = _factory.Create())
            {
                return uow.Repository<PersonRole>().Get(r => r.Name == rolename).Any();
            }
        }

        public IEnumerable<string> ReadUsersInRole(string rolename)
        {
            var result = new List<string>();

            using (var uow = _factory.Create())
            {
                var roledb = uow.Repository<PersonRole>().Get(r => r.Name == rolename).FirstOrDefault();
                if (roledb != null)
                    result.AddRange(roledb.People.ToList().Select(user => user.Login));
            }
            return result;
        }

        public bool IsUserInRole(string login, string rolename)
        {
            using (var uow = _factory.Create())
            {
                return uow.Repository<Person>().Get(u => u.Login == login && u.PersonRole.Name == rolename).Any();
            }
        }

        public void AddUsersToRole(IEnumerable<string> users, string role)
        {
            using (var uow = _factory.Create())
            {
                var roledb = uow.Repository<PersonRole>().Get(r => r.Name == role).FirstOrDefault();
                if (roledb == null) return;
                var userdbs = uow.Repository<Person>().Get(u => users.Contains(u.Login)).ToList();
                for (int i = 0; i < userdbs.Count; i++)
                {
                    userdbs[i].PersonRole = roledb;
                    uow.Repository<Person>().Update(userdbs[i]);
                    
                }
                uow.Save();
            }
        }

        public PersonRoleDto GetByName(string roleName)
        {
            using (var uow = _factory.Create())
            {
                var entity = uow.Repository<PersonRole>().Get(rn => rn.Name == roleName).FirstOrDefault();
                var entityDto = AutoMapper.Mapper.Map<PersonRoleDto>(entity);
                return entityDto;
            }
        }
    }
}

using ELib.BL.DtoEntities;
using ELib.BL.Mapper.Abstract;
using ELib.BL.Services.Abstract;
using ELib.DAL.Infrastructure.Abstract;
using ELib.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.Services.Concrete
{
    public class ProfileService : BaseService<Person,PersonDto>, IProfileService
    {
        public ProfileService(IUnitOfWorkFactory factory, IMapper<Person, PersonDto> mapper)
            : base(factory, mapper)
        { }

        public List<PersonDto> GetUsersByArray(int[] ids)
        {
            using (var uow = _factory.Create())
            {
                List<PersonDto> PersonList = new List<PersonDto>();

                foreach(int id in ids)
                {
                    var Person = uow.Repository<Person>().GetById(id);
                    if(Person == null)
                    {
                        continue;
                    }
                    else
                    {
                        PersonList.Add(_mapper.Map(Person));
                    }
                }
                return PersonList;
            }
        }
    }
}
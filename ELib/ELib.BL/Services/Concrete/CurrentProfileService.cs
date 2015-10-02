using ELib.BL.DtoEntities;
using ELib.BL.Mapper.Abstract;
using ELib.BL.Services.Abstract;
using ELib.DAL.Infrastructure.Abstract;
using ELib.DAL.Infrastructure.Concrete;
using ELib.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.Services.Concrete
{
    public class CurrentProfileService : BaseService<Person,CurrentPersonDto>, ICurrentProfileService
    {
        public CurrentProfileService(IUnitOfWorkFactory factory, IMapper<Person, CurrentPersonDto> mapper)
            : base(factory, mapper)
        { }

        public CurrentPersonDto GetByApplicationUserId(string id)
        {
            using (var uow =_factory.Create())
            {
                //ApplicationUser au = uow.Repository<ApplicationUser>().Get(x => x.Id == id).FirstOrDefault();
                //if (au == null)
                //    return null;
                Person p = uow.Repository<Person>().Get(pers => pers.ApplicationUserId == id).FirstOrDefault();
                return _mapper.Map(p);
            }
        }
    }
}

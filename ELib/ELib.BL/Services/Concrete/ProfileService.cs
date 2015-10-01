using ELib.BL.DtoEntities;
using ELib.BL.Mapper;
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
    }
}

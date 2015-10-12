using ELib.BL.DtoEntities;
using ELib.BL.Mapper.Abstract;
using ELib.BL.Services.Abstract;
using ELib.DAL.Infrastructure.Abstract;
using ELib.Domain.Entities;
using System.Linq;

namespace ELib.BL.Services.Concrete
{
    public class LanguageService : BaseService<Language, LanguageDto>, ILanguageService
    {
        public LanguageService(IUnitOfWorkFactory factory, IMapper<Language, LanguageDto> mapper)
            : base(factory, mapper)
        {
            _defaultSort = q => q.OrderBy(l => l.Name);
        }
    }
}

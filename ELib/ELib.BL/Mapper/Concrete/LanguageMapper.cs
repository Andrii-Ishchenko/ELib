using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using ELib.BL.Mapper.Abstract;

namespace ELib.BL.Mapper.Concrete
{
    public class LanguageMapper : IMapper<Language, LanguageDto>
    {
        public Language Map(LanguageDto input)
        {
            return new Language()
            {
                Id = input.Id,
                Name = input.Name
            };
        }

        public LanguageDto Map(Language input)
        {
            return new LanguageDto()
            {
                Id = input.Id,
                Name = input.Name
            };
        }
    }
}

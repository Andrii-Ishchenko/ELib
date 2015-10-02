using ELib.BL.DtoEntities;
using ELib.Domain.Entities;

namespace ELib.BL.Mapper.Concrete
{
    public class GenreMapper : IMapper<Genre, GenreDto>
    {
        public Genre Map(GenreDto input)
        {
            return new Genre()
            {
                Id = input.Id,
                Name = input.Name
            };
        }

        public GenreDto Map(Genre input)
        {
            return new GenreDto()
            {
                Id = input.Id,
                Name = input.Name
            };
        }
    }
}

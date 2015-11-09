using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using ELib.BL.Mapper.Abstract;

namespace ELib.BL.Mapper.Concrete
{
    public class GenreMapper : IMapper<Genre, GenreDto>
    {
        public Genre Map(GenreDto input)
        {
            if (input == null)
                return null;
            return new Genre()
            {
                Id = input.Id,
                Name = input.Name,
                State = input.State
            };
        }

        public GenreDto Map(Genre input)
        {
            if (input == null)
                return null;
            return new GenreDto()
            {
                Id = input.Id,
                Name = input.Name,
                State = input.State
            };
        }
    }
}

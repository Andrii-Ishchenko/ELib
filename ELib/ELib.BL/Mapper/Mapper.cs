using ELib.BL.DtoEntities;
using ELib.Domain.Entities;

namespace ELib.BL.Mapper
{
    public static class Mapper
    {
        public static GenreDto ToGenreDto(this Genre genre)
        {
            GenreDto genreDto = new GenreDto()
            {
                Id = genre.Id,
                Name = genre.Name
            };

            return genreDto;
        }

        public static Genre ToGenre(this GenreDto genreDto)
        {
            Genre genre = new Genre()
            {
                Id = genreDto.Id,
                Name = genreDto.Name
            };

            return genre;
        }
    }
}

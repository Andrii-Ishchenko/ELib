using ELib.BL.DtoEntities;
using ELib.Domain.Entities;

namespace ELib.BL.Mapper
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            configureGenreMapping();
        }

        private static void configureGenreMapping()
        {
            AutoMapper.Mapper.CreateMap<Genre, GenreDto>();
            AutoMapper.Mapper.CreateMap<GenreDto, Genre>();

            AutoMapper.Mapper.CreateMap<Publisher, PublisherDto>();
            AutoMapper.Mapper.CreateMap<PublisherDto, Publisher>();


            AutoMapper.Mapper.CreateMap<Author, AuthorDto>();
            AutoMapper.Mapper.CreateMap<AuthorDto, Author>();
        }
    }
}

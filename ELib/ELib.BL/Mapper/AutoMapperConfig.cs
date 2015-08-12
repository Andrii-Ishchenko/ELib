using ELib.BL.DtoEntities;
using ELib.Domain.Entities;

namespace ELib.BL.Mapper
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            configureGenreMapping();
            configureCommentMapping();
        }

        private static void configureGenreMapping()
        {
            AutoMapper.Mapper.CreateMap<Genre, GenreDto>();
            AutoMapper.Mapper.CreateMap<GenreDto, Genre>();
        }

        private static void configureCommentMapping()
        {
            AutoMapper.Mapper.CreateMap<Comment, CommentDto>();
            AutoMapper.Mapper.CreateMap<CommentDto, CommentDto>();
        }
    }
}

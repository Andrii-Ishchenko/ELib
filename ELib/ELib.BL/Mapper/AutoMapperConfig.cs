using ELib.BL.DtoEntities;
using ELib.Domain.Entities;

namespace ELib.BL.Mapper
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            configureGenreMapping();
            configurePersonRoleMapping();
            configurePersonMapping();
        }

        private static void configureGenreMapping()
        {
            AutoMapper.Mapper.CreateMap<Genre, GenreDto>();
            AutoMapper.Mapper.CreateMap<GenreDto, Genre>();
        }
        private static void configurePersonRoleMapping()
        {
            AutoMapper.Mapper.CreateMap<PersonRole, PersonRoleDto>();
            AutoMapper.Mapper.CreateMap<PersonRoleDto, PersonRole>();
        }
        private static void configurePersonMapping()
        {
            AutoMapper.Mapper.CreateMap<Person, PersonDto>();
            AutoMapper.Mapper.CreateMap<PersonDto, Person>();
        }
    }
}

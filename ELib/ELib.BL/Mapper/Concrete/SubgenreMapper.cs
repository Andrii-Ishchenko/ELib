using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using System;
using ELib.BL.Mapper.Abstract;

namespace ELib.BL.Mapper.Concrete
{
    public class SubgenreMapper : IMapper<Subgenre, SubgenreDto>
    {
        public Subgenre Map(SubgenreDto input)
        {
            return new Subgenre()
            {
                Id = input.Id,
                Name = input.Name
            };
        }

        public SubgenreDto Map(Subgenre input)
        {
            return new SubgenreDto()
            {
                Id = input.Id,
                Name = input.Name
            };
        }
    }
}

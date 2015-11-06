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
            if (input == null)
                return null;
            return new Subgenre()
            {
                Id = input.Id,
                Name = input.Name,
                State = input.State
            };
        }

        public SubgenreDto Map(Subgenre input)
        {
            if (input == null)
                return null;
            return new SubgenreDto()
            {
                Id = input.Id,
                Name = input.Name,
                State = input.State
            };
        }
    }
}

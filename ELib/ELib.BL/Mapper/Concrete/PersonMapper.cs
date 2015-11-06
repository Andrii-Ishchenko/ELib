using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using ELib.BL.Mapper.Abstract;


namespace ELib.BL.Mapper.Concrete
{
    public class PersonMapper : IMapper<Person, PersonDto>
    {
        public PersonDto Map(Person input)
        {
            if (input == null)
                return null;
            return new PersonDto() { Id = input.Id,
                                     FirstName = input.FirstName,
                                     LastName = input.LastName,
                                     ImageHash = input.ImageHash,
                                     ApplicationUserId =input.ApplicationUserId,
                                     State = input.State
            };
        }

        public Person Map(PersonDto input)
        {

            if (input == null)
                return null;
            return new Person() { Id = input.Id,
                                  FirstName = input.FirstName,
                                  LastName = input.LastName,
                                  ImageHash = input.ImageHash,
                                  ApplicationUserId = input.ApplicationUserId,
                                  State = input.State
            };
        }
    }
}
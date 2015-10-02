using ELib.BL.DtoEntities;
using ELib.Domain.Entities;


namespace ELib.BL.Mapper.Concrete
{
    public class PersonMapper : IMapper<Person, PersonDto>
    {
        public PersonDto Map(Person input)
        {
            return new PersonDto() { Id = input.Id, FirstName = input.FirstName, LastName = input.LastName, ImageHash = input.ImageHash, ApplicationUserId =input.ApplicationUserId};
        }

        public Person Map(PersonDto input)
        {
            return new Person() { Id = input.Id, FirstName = input.FirstName, LastName = input.LastName, ImageHash = input.ImageHash, ApplicationUserId = input.ApplicationUserId };
        }
    }
}
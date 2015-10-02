using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using ELib.BL.Mapper.Abstract;

namespace ELib.BL.Mapper.Concrete
{
    public class PublisherMapper : IMapper<Publisher, PublisherDto>
    {
        public Publisher Map(PublisherDto input)
        {
            return new Publisher() { Id = input.Id, Name = input.Name };
        }

        public PublisherDto Map(Publisher input)
        {
            return new PublisherDto() { Id = input.Id, Name = input.Name };
        }
    }
}

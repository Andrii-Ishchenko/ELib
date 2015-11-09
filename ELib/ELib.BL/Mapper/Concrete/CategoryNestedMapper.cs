using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using ELib.BL.Mapper.Abstract;

namespace ELib.BL.Mapper.Concrete
{
    public class CategoryNestedMapper : IMapper<CategoryDto, CategoryNestedDto>
    {
        public CategoryDto Map(CategoryNestedDto input)
        {
            if (input == null)
                return null;
            return new CategoryDto()
            {
                Id = input.Id,
                Name = input.Name,
                BookCount = input.BookCount,
                Level = input.Level,
                ParentId = input.ParentId,
                State = input.State
            };
        }

        public CategoryNestedDto Map(CategoryDto input)
        {
            if (input == null)
                return null;
            return new CategoryNestedDto()
            {
                Id = input.Id,
                Name = input.Name,
                BookCount = input.BookCount,
                Level = input.Level,
                ParentId = input.ParentId,
                State = input.State
            };
        }
    }
}

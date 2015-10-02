using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using ELib.BL.Mapper.Abstract;

namespace ELib.BL.Mapper.Concrete
{
    public class CategoryNestedMapper : IMapper<Category, CategoryNestedDto>
    {
        public Category Map(CategoryNestedDto input)
        {
            return new Category()
            {
                Id = input.Id,
                Name = input.Name,
                BookCount = input.BookCount,
                Level = input.Level,
                ParentId = input.ParentId
            };
        }

        public CategoryNestedDto Map(Category input)
        {
            return new CategoryNestedDto()
            {
                Id = input.Id,
                Name = input.Name,
                BookCount = input.BookCount,
                Level = input.Level,
                ParentId = input.ParentId
            };
        }
    }
}

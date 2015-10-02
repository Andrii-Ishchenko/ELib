using ELib.BL.DtoEntities;
using ELib.Domain.Entities;

namespace ELib.BL.Mapper.Concrete
{
    public class CategoryMapper : IMapper<Category, CategoryDto>
    {
        public Category Map(CategoryDto input)
        {
            return new Category()
            {
                Id = input.Id,
                Name = input.Name,
                ParentId = input.ParentId,
                Level = input.Level,
                BookCount = input.BookCount
            };
        }

        public CategoryDto Map(Category input)
        {
            return new CategoryDto()
            {
                Id = input.Id,
                Name = input.Name,
                ParentId = input.ParentId,
                Level = input.Level,
                BookCount = input.BookCount
            };
        }
    }
}

using ELib.BL.DtoEntities;

namespace ELib.BL.Mapper.Concrete
{
    public class CategoryNestedMapper : IMapper<CategoryDto, CategoryNestedDto>
    {
        public CategoryDto Map(CategoryNestedDto input)
        {
            return new CategoryDto()
            {
                Id = input.Id,
                Name = input.Name,
                BookCount = input.BookCount,
                Level = input.Level,
                ParentId = input.ParentId
            };
        }

        public CategoryNestedDto Map(CategoryDto input)
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

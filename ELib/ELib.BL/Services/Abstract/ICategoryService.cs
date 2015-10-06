using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using System.Collections.Generic;
namespace ELib.BL.Services.Abstract
{
    public interface ICategoryService :IBaseService<Category, CategoryDto>
    {
        List<CategoryNestedDto> GetNested();
        List<CategoryDto> GetAllChildrenForCategory(int Id);
    }
}

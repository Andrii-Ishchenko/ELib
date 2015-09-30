using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.Services.Abstract
{
    public interface ICategoryService :IBaseService<Category,CategoryDto>
    {
        List<CategoryNestedDto> GetNested();
        List<CategoryDto> GetAllChildrenForCategory(int Id);

    }
}

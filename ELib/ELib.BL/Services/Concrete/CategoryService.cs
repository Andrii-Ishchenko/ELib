using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using ELib.DAL.Infrastructure.Abstract;
using ELib.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.Services.Concrete
{
    public class CategoryService : BaseService<Category,CategoryDto>, ICategoryService
    {
        public CategoryService(IUnitOfWorkFactory factory) 
            :base(factory){ }

        public List<CategoryNestedDto> GetNested()
        {
            List<CategoryDto> mixed = base.GetAll().ToList();
            CategoryNestedDto item = new CategoryNestedDto();
            FillNestedList(mixed, item,null);
            return item.Children;
        }

        private void FillNestedList(List<CategoryDto> list, CategoryNestedDto item, int? itemId)
        {
            item.Children = new List<CategoryNestedDto>();
            List<CategoryDto> children = GetChildren(list, itemId);
            foreach (CategoryDto child in children)
            {
                item.Children.Add(AutoMapper.Mapper.Map<CategoryNestedDto>(child));
            }
            list.RemoveAll(p => children.Contains(p));

            foreach (CategoryNestedDto t in item.Children)
                FillNestedList(list, t,t.Id);
        }

        private List<CategoryDto> GetChildren(List<CategoryDto> list, int? itemId)
        {
            if (itemId == null)
                return list.Where(m => m.ParentId == null).ToList();

            return list.Where(m => m.ParentId == itemId).ToList();
        }
    }
}

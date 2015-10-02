using ELib.BL.DtoEntities;
using ELib.BL.Mapper.Abstract;
using ELib.BL.Services.Abstract;
using ELib.DAL.Infrastructure.Abstract;
using ELib.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ELib.BL.Services.Concrete
{
    public class CategoryService : BaseService<Category, CategoryDto>, ICategoryService
    {
        IMapper<CategoryDto, CategoryNestedDto> _categoryNestedMapper;

        public CategoryService(IUnitOfWorkFactory factory, IMapper<Category, CategoryDto> category,
            IMapper<CategoryDto, CategoryNestedDto> categoryNested) : base(factory, category)
        {
            _categoryNestedMapper = categoryNested;
        }

        public List<CategoryNestedDto> GetNested()
        {
            List<CategoryDto> mixed = GetAll().ToList();
            CategoryNestedDto item = new CategoryNestedDto();
            FillNestedList(mixed, item, null);
            return item.Children;
        }

        public List<CategoryDto> GetAllChildrenForCategory(int CategoryId)
        {
            List<CategoryDto> all = GetAll().ToList();

            CategoryDto category = all.Where(c => c.Id == CategoryId).FirstOrDefault();
            if (category == null)
                return null;
            return GetChildrenForCategoryRecursive(category, all);

        }

        private List<CategoryDto> GetChildrenForCategoryRecursive(CategoryDto c, List<CategoryDto> all)
        {
            List<CategoryDto> temp = new List<CategoryDto>();
            List<CategoryDto> children = all.Where(cat => cat.ParentId == c.Id).ToList();
            temp.AddRange(children);
            foreach (CategoryDto cat in children)
            {
                temp.AddRange(GetChildrenForCategoryRecursive(cat, all));
            }
            return temp;
        }

        private void FillNestedList(List<CategoryDto> list, CategoryNestedDto item, int? itemId)
        {
            item.Children = new List<CategoryNestedDto>();
            List<CategoryDto> children = GetChildren(list, itemId);
            foreach (CategoryDto child in children)
            {
                item.Children.Add(_categoryNestedMapper.Map(child));
            }
            list.RemoveAll(p => children.Contains(p));

            foreach (CategoryNestedDto t in item.Children)
                FillNestedList(list, t, t.Id);
        }

        private List<CategoryDto> GetChildren(List<CategoryDto> list, int? itemId)
        {
            if (itemId == null)
                return list.Where(m => m.ParentId == null).ToList();

            return list.Where(m => m.ParentId == itemId).ToList();
        }
    }
}

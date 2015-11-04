﻿using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using ELib.BL.Mapper.Abstract;
using System;

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
                BookCount = input.BookCount,
                State = input.State
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
                BookCount = input.BookCount,
                State = input.State
            };
        }
    }
}

using System.Collections.Generic;

namespace ELib.BL.DtoEntities
{
    public class CategoryNestedDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public int Level { get; set; }

        public int BookCount { get; set; }

        public List<CategoryNestedDto> Children { get; set; }
    }
}

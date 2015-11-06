using System.Collections.Generic;
using ELib.BL.DtoEntities.Abstract;
using System.ComponentModel.DataAnnotations;
using ELib.Common;

namespace ELib.BL.DtoEntities
{
    public class CategoryNestedDto : IDtoEntityState
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public int Level { get; set; }

        public int BookCount { get; set; }

        public List<CategoryNestedDto> Children { get; set; }

        [Required]
        public LibEntityState State { get; set; }
    }
}

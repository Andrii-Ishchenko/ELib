using ELib.BL.DtoEntities.Abstract;
using ELib.Common;
using System.ComponentModel.DataAnnotations;

namespace ELib.BL.DtoEntities
{
    public class SubgenreDto : IDtoEntityState
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        public LibEntityState State { get; set; }
    }
}

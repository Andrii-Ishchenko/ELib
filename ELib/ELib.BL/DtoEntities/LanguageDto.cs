using System.ComponentModel.DataAnnotations;
using ELib.BL.DtoEntities.Abstract;
using ELib.Common;

namespace ELib.BL.DtoEntities
{
    public class LanguageDto : IDtoEntityState
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        public LibEntityState State { get; set; }
    }
}

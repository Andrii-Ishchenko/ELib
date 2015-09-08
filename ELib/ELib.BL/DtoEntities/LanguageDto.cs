using System.ComponentModel.DataAnnotations;

namespace ELib.BL.DtoEntities
{
    class LanguageDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }
    }
}

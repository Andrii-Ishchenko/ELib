using ELib.Common;
using ELib.BL.DtoEntities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace ELib.BL.DtoEntities
{
    public class PublisherDto : IDtoEntityState
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        public LibEntityState State { get; set; }
    }
}

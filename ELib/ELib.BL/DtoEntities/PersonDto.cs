using ELib.Common;
using ELib.BL.DtoEntities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace ELib.BL.DtoEntities
{
    public class PersonDto : IDtoEntityState
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public string ImageHash { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [Required]
        public LibEntityState State { get; set; }

    }
}

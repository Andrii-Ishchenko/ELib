using ELib.Common;
using ELib.BL.DtoEntities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace ELib.BL.DtoEntities
{
    public class CurrentPersonDto : IDtoEntityState
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public string Email { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        public string ImageHash { get; set; }

        public string ApplicationUserId { get; set; }

        [Required]
        public LibEntityState State { get; set; }

    }
}

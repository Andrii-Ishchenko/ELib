using ELib.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ELib.BL.DtoEntities.Abstract;

namespace ELib.BL.DtoEntities
{
    public class AuthorDto : IDtoEntityState
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime? DeathDate { get; set; }

        public string Description { get; set; }

        public string ImageHash { get; set; }

        [Required]
        public LibEntityState State { get; set; }
    }
}

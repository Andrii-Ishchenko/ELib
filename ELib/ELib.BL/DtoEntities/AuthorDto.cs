using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ELib.BL.DtoEntities
{
    public class AuthorDto
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
    }
}

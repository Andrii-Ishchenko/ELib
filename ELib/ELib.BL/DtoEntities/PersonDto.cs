using ELib.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.DtoEntities
{
    public class PersonDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(30)]
        public string Phone { get; set; }

        [Required]
        [StringLength(50)]
        public string Login { get; set; }

        //[Column(TypeName = "date")]
        public DateTime RegistrationDate { get; set; }

        //[Column(TypeName = "date")]
        public DateTime? LastActivationDate { get; set; }

        public string ImageHash { get; set; }

        public string AplicationUserId { get; set; }

        public virtual ApplicationUser PersonUser { get; set; }
    }
}

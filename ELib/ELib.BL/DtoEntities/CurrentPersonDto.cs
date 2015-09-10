using ELib.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.DtoEntities
{
    public class CurrentPersonDto
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

        public string ImagePath { get; set; }

        public string ApplicationUserId { get; set; }

    }
}

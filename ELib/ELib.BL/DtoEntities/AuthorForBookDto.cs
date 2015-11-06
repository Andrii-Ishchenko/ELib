using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ELib.Common;
using ELib.BL.DtoEntities.Abstract;

namespace ELib.BL.DtoEntities
{
    public class AuthorForBookDto : IDtoEntityState
    {
        public int Id { get; set; }

        public int BookAuthorsId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public LibEntityState State { get; set; }
    }
}

using ELib.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ELib.BL.DtoEntities.Abstract;

namespace ELib.BL.DtoEntities
{
    public class AuthorsInBookDto : IDtoEntityState
    {
        public int BookId { get; set; }

        public List<int> AuthorsId = new List<int>();

        [Required]
        public LibEntityState State { get; set; }
    }
}

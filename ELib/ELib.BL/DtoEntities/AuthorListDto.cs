using ELib.Common;
using System.ComponentModel.DataAnnotations;
using ELib.BL.DtoEntities.Abstract;

namespace ELib.BL.DtoEntities
{
    public class AuthorListDto : IDtoEntityState
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public LibEntityState State { get; set; }
    }
}

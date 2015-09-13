using ELib.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ELib.BL.DtoEntities
{
    public partial class BookInstanceDto
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string FileHash { get; set; }

        [StringLength(400)]
        public string FileName { get; set; }
    }
}

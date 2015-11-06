using ELib.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.IO;
using ELib.BL.DtoEntities.Abstract;
using ELib.Common;

namespace ELib.BL.DtoEntities
{
    public partial class BookInstanceDto : IDtoEntityState
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string FileHash { get; set; }

        [StringLength(400)]
        public string FileName { get; set; }

        public int DownloadCount { get; set; }

        public string Extension
        {
            get { return Path.GetExtension(FileName).Replace(".", string.Empty).ToUpperInvariant(); }
        }

        [Required]
        public LibEntityState State { get; set; }
    }
}

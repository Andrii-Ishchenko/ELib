using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ELib.BL.DtoEntities.Abstract;
using ELib.Common;

namespace ELib.BL.DtoEntities
{
    public class BookInListDto : IDtoEntityState
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public int PublisherId { get; set; }

        [Range(0, 9999)]
        public int PublishYear { get; set; }

        public DateTime AdditionDate { get; set; }

        public string ImageHash { get; set; }

        [StringLength(40)]
        public string PublisherName { get; set; }

        public ICollection<AuthorDto> Authors { get; set; }

        public int Rating { get; set; }

        [Required]
        public LibEntityState State { get; set; }

    }
}

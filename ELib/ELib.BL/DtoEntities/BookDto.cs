using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ELib.BL.DtoEntities.Abstract;
using ELib.Common;

namespace ELib.BL.DtoEntities
{
    public class BookDto : IDtoEntityState
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public int PublishLangId { get; set; }

        public int? OriginalLangId { get; set; }

        public int? TotalPages { get; set; }

        [StringLength(20)]
        public string Isbn { get; set; }

        public int PublisherId { get; set; }

        public int SubgenreId { get; set; }

        public int CategoryId { get; set; }

        [Range(0, 9999)]
        public int PublishYear { get; set; }

        public DateTime AdditionDate { get; set; }

        public string ImageHash { get; set; }

        public string Description { get; set; }

        public LanguageDto Language { get; set; }

        public LanguageDto Language1 { get; set; }

        public PublisherDto Publisher{ get; set; }

        public SubgenreDto Subgenre { get; set; }

        public CategoryDto Category { get; set; }

        public ICollection<AuthorForBookDto> Authors { get; set; }

        public ICollection<GenreForBookDto> Genres { get; set; }

        public ICollection<BookInstanceDto> BookInstances { get; set; }

        public int Rating { get; set; }

        public int TotalDownloadCount { get; set; }

        public int TotalViewCount { get; set; }

        [Required]
        [EnumDataType (typeof(LibEntityState))]
        public LibEntityState State { get; set; }
    }
}

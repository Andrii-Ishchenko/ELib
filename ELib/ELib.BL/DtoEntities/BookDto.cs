﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ELib.BL.DtoEntities
{
    public class BookDto
    {

        public int Id {get; set;}

        [Required]
        [StringLength(200)]
        public string Title { get; set;}

        public int PublishLangId { get; set; }

        public int? OriginalLangId { get; set; }

        public int? TotalPages { get; set; }

        [StringLength(20)]
        public string Isbn { get; set; }

        public int PublisherId { get; set; }

        public int SubgenreId { get; set;  }

        public DateTime? PublishYear { get; set; }

        public String ImageHash { get; set; }

        public string Description { get; set; }

        public string LanguageName { get; set; }

        public string Language1Name { get; set; }

        public string PublisherName { get; set; }

        public string SubgenreName { get; set; }

        public int SumRatingValue { get; set; }

        public ICollection<string> Authors { get; set; }

        public ICollection<int> AuthorsIds { get; set; }

        public ICollection<string> GenresNames { get; set; }

        public ICollection<int> GenresIds { get; set; }

        public ICollection<BookInstanceDto> BookInstances { get; set; }

        public int Rating { get; set; }

        public int TotalDownloadCount { get; set; }

        public int TotalViewCount { get; set; }

        //Status????
    }
}

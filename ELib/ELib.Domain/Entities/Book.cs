namespace ELib.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Book")]
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            BookFormats = new HashSet<BookFormat>();
            BookGenres = new HashSet<BookGenre>();
            RatingBooks = new HashSet<RatingBook>();
            UserBookStatus = new HashSet<UserBookStatus>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public int PublishLangId { get; set; }

        public int? OriginalLangId { get; set; }

        public int? TotalPage { get; set; }

        [StringLength(20)]
        public string Isbn { get; set; }

        public int PublisherId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PublishYear { get; set; }

        public byte[] Picture { get; set; }

        public string Descript { get; set; }

        public virtual Language Language { get; set; }

        public virtual Language Language1 { get; set; }

        public virtual Publisher Publisher { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookFormat> BookFormats { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookGenre> BookGenres { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RatingBook> RatingBooks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserBookStatus> UserBookStatus { get; set; }
    }
}

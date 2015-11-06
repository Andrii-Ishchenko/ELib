namespace ELib.Domain.Entities
{
    using Abstract;
    using ELib.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Book")]
    public partial class Book : IEntityState
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            BookInstances = new HashSet<BookInstance>();
            BookGenres = new HashSet<BookGenre>();
            BookAuthors = new HashSet<BookAuthor>();
            RatingBooks = new HashSet<RatingBook>();
            UserBookStatus = new HashSet<UserBookStatus>();
        }

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

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime AdditionDate { get; set; }

        [Range(0, 9999)]
        public int PublishYear { get; set; }

        public string ImageHash { get; set; }

        public string Description { get; set; }

        public virtual Subgenre Subgenre { get; set; }

        public virtual Language PublishLanguage { get; set; }

        public virtual Language OriginalLanguage { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual Category Category { get; set; }

        public int SumRatingValue { get; set; }

        public int TotalDownloadCount { get; set; }

        public int TotalViewCount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookInstance> BookInstances { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookGenre> BookGenres { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RatingBook> RatingBooks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserBookStatus> UserBookStatus { get; set; }

        public virtual ICollection<Favorite> Favorites { get; set; }

        [NotMapped]
        public LibEntityState State { get; set; }
    }
}

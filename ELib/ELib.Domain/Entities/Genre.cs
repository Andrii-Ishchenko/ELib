namespace ELib.Domain.Entities
{
    using Abstract;
    using ELib.Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Genre")]
    public partial class Genre : IEntityState
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Genre()
        {
            AuthorGenres = new HashSet<AuthorGenre>();
            BookGenres = new HashSet<BookGenre>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AuthorGenre> AuthorGenres { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookGenre> BookGenres { get; set; }

        [NotMapped]
        public LibEntityState State { get; set; }
    }
}

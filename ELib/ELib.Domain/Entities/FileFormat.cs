namespace ELib.Domain.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("FileFormat")]
    public partial class FileFormat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FileFormat()
        {
            BookFormats = new HashSet<BookFormat>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookFormat> BookFormats { get; set; }
    }
}

namespace ELib.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Person")]
    public partial class Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {
            RatingBooks = new HashSet<RatingBook>();
            RatingComments = new HashSet<RatingComment>();
            UserBookStatus = new HashSet<UserBookStatus>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(30)]
        public string Phone { get; set; }

        [Required]
        [StringLength(50)]
        public string Login { get; set; }

        [Column(TypeName = "date")]
        public DateTime RegistrationDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastActivationDate { get; set; }

        public string ImageHash { get; set; }

        public string AplicationUserId { get; set; }

        public virtual ApplicationUser PersonUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RatingBook> RatingBooks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RatingComment> RatingComments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserBookStatus> UserBookStatus { get; set; }
    }
}

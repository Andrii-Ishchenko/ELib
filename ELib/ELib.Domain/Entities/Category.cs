﻿namespace ELib.Domain.Entities
{
    using Abstract;
    using ELib.Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Category")]
    public partial class Category : IEntityState
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int? ParentId { get; set; }

        public int Level { get; set; }

        public int BookCount { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        [NotMapped]
        public LibEntityState State { get; set; }
    }
}

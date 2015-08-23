using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.DtoEntities
{
    public class CommentDto
    {
        public int Id { get; set; }

        [StringLength(400)]
        public string Text { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        //[Column(TypeName = "date")]
        public DateTime CommentDate { get; set; }

        public int SumLike { get; set; }

        public int SumDisLike { get; set; }
    }
}
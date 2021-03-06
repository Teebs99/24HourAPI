using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Reply
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public Guid AuthorId { get; set; }
        //public virtual List<Reply> { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        //public DateTimeOffset? ModifiedUtc { get; set; }

        // Foreign key for Comment
        [ForeignKey(nameof(Comment))]
        public int CommentId { get; set; }
        public Comment Comment { get; set; }

    }
}
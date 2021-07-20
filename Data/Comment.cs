using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Comment
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

        public int PostId { get; set; }
    }
}

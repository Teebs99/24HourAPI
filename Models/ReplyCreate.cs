using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ReplyCreate
    {
        [Required]
        [MinLength(2, ErrorMessage = "Please enter a reply with at least 2 characters.")]
        [MaxLength(250, ErrorMessage = "Too many characters in this field.")]
        public string Content { get; set; }
        [Required]
        public int CommentId { get; set; }
    }
}
//{ "mode":"full","isActive":false}

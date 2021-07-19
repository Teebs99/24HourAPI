using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CreatePost
    {
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Bar_rating.Models
{
    public partial class Bar
    {
        public Bar()
        {
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string? Description { get; set; }
        public byte[]? BarImage { get; set; }
        public string? Name { get; set; }
        [NotMapped]
        [Display(Name = "Bar Image")]
        public IFormFile BarImageFile { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}

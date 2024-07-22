using System;
using System.Collections.Generic;

namespace Bar_rating.Models
{
    public partial class Review
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? BarId { get; set; }
        public string? ReviewText { get; set; }

        public virtual Bar? Bar { get; set; }
        public virtual User? User { get; set; }
    }
}

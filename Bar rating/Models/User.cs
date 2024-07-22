using System;
using System.Collections.Generic;

namespace Bar_rating.Models
{
    public partial class User
    {
        public User()
        {
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? IsAdmin { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}

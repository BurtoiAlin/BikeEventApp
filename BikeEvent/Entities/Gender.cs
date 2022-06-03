using System;
using System.Collections.Generic;

#nullable disable

namespace BikeEvent.Entities
{
    public partial class Gender
    {
        public Gender()
        {
            Categories = new HashSet<Category>();
            Users = new HashSet<User>();
        }

        public Guid GenderId { get; set; }
        public string GenderName { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}

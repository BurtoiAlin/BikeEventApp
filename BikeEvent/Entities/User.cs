using System;
using System.Collections.Generic;

#nullable disable

namespace BikeEvent.Entities
{
    public partial class User
    {
        public User()
        {
            Results = new HashSet<Result>();
            UserClubs = new HashSet<UserClub>();
            UserEvents = new HashSet<UserEvent>();
        }

        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public Guid GenderId { get; set; }
        public string Picture { get; set; }
        public Guid ClubId { get; set; }
        public string Password { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Result> Results { get; set; }
        public virtual ICollection<UserClub> UserClubs { get; set; }
        public virtual ICollection<UserEvent> UserEvents { get; set; }
    }
}

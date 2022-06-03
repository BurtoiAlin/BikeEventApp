using System;
using System.Collections.Generic;

#nullable disable

namespace BikeEvent.Entities
{
    public partial class Club
    {
        public Club()
        {
            UserClubs = new HashSet<UserClub>();
        }

        public Guid ClubId { get; set; }
        public string ClubName { get; set; }
        public string ClubDescription { get; set; }

        public virtual ICollection<UserClub> UserClubs { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace BikeEvent.Entities
{
    public partial class UserClub
    {
        public Guid UserClubId { get; set; }
        public Guid UserId { get; set; }
        public Guid ClubId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? ExitDate { get; set; }

        public virtual Club Club { get; set; }
        public virtual User User { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace BikeEvent.Entities
{
    public partial class Event
    {
        public Event()
        {
            Results = new HashSet<Result>();
            UserEvents = new HashSet<UserEvent>();
        }

        public Guid EventId { get; set; }
        public string EventName { get; set; }
        public string EventPic { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EventDescription { get; set; }
        public string MapPath { get; set; }

        public virtual ICollection<Result> Results { get; set; }
        public virtual ICollection<UserEvent> UserEvents { get; set; }
    }
}

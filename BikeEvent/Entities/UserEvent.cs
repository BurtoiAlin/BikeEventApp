﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BikeEvent.Entities
{
    public partial class UserEvent
    {
        public Guid UserEventsId { get; set; }
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }

        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
    }
}

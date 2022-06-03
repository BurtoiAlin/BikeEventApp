using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeEvent.Models
{
    public class AddEvent
    {
        public string EventName { get; set; }
        public string? EventPic { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EventDescription { get; set; }
        public string? MapPath { get; set; }
    }
}

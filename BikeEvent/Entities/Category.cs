using System;
using System.Collections.Generic;

#nullable disable

namespace BikeEvent.Entities
{
    public partial class Category
    {
        public Guid CategoryId { get; set; }
        public Guid CategoryTypeId { get; set; }
        public Guid GenderId { get; set; }
        public Guid CategoryAgeId { get; set; }

        public virtual CategoryAge CategoryAge { get; set; }
        public virtual CategoryType CategoryType { get; set; }
        public virtual Gender Gender { get; set; }
    }
}

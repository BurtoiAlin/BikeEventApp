using System;
using System.Collections.Generic;

#nullable disable

namespace BikeEvent.Entities
{
    public partial class CategoryAge
    {
        public CategoryAge()
        {
            Categories = new HashSet<Category>();
        }

        public Guid CategoryAgeId { get; set; }
        public int StartingAge { get; set; }
        public int? EndAge { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}

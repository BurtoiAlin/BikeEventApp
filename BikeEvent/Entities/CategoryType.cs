using System;
using System.Collections.Generic;

#nullable disable

namespace BikeEvent.Entities
{
    public partial class CategoryType
    {
        public CategoryType()
        {
            Categories = new HashSet<Category>();
        }

        public Guid CategoryTypeId { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class BaseEntity
    {
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime LastModifiedOn { get; set; } = DateTime.UtcNow;
        public Guid CreatedBy { get; set; }
        public Guid LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public int TotalCount { get; set; }
    }
}

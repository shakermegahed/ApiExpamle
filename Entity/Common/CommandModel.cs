using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class CommandModel
    {
        public CommandModel()
        {
        }

        public Guid EntityId { get; set; }
        public Guid LastModifiedBy { get; set; }
    }
}

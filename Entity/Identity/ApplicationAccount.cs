
using EntityDatabaseEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class ApplicationAccount : AccountEntity
    {
        public ApplicationAccount()
        {
            AccountId = Guid.NewGuid();
        }

        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string RoleName { get; set; }
    }
}

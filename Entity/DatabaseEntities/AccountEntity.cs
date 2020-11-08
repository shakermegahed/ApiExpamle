using Entity;
using System;
using System.Collections.Generic;
using System.Text;

using static CRUD.Core.Core;

namespace EntityDatabaseEntities
{
    public class AccountEntity : BaseEntity
    {
        public Guid AccountId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AccountStatus Status { get; set; }
        public string PasswordHash { get; set; }
    }
}

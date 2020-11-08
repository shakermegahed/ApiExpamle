using System;
using System.Collections.Generic;
using System.Text;
using static CRUD.Core.Core;

namespace Entity.DomainModel
{
    public class AccountModel: BaseModel
    {
        public Guid? AccountId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public AccountStatus Status { get; set; }
        public string AccountRole { get; set; }

    }
}

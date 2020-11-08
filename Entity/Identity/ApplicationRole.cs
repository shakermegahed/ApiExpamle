using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class ApplicationRole
    {
        public ApplicationRole()
        {

        }

        public ApplicationRole(string roleName)
        {
            this.RoleName = roleName;
        }

        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string NormalizedRoleName { get; set; }
    }
}

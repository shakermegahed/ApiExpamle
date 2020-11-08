
using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRoleRepository
    {
        Task Add(ApplicationRole Role);
        Task<ApplicationRole> GetByRoleName(string RoleName);
        Task<ApplicationRole> GetById(Guid RoleId);
        Task Update(ApplicationRole Role);
        Task Delete(Guid RoleId);
        Task AddToRoleAsync(string RoleName, Guid AccountId, Guid? CreatedBy = null);
        Task<IList<string>> GetRolesAsync(Guid AccountId);
        Task<bool> IsInRoleAsync(string RoleName, Guid AccountId);
    }
}

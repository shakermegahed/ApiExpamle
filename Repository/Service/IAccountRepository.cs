
using Entity;
using Entity.DomainModel;
using EntityDatabaseEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CRUD.Core.Core;

namespace Repository
{
    public interface IAccountRepository
    {
        Task<IEnumerable<AccountModel>> GetAccountByRoleName(string roleName, string SearchKeyWord, PagedQueryParameters pagedQueryParameters);
        Task Add(ApplicationAccount account);
        Task Delete(Guid adminId);
        Task Update(ApplicationAccount account);
        Task UpdateIsDeleted(AccountEntity account);
        Task UpdateStatus(Guid accountId, AccountStatus status);
        Task<AccountEntity> Login(string email, string passwordHashed);
        Task<ApplicationAccount> GetById(Guid accountId);
        Task<ApplicationAccount> GetByEmail(string email);
        Task<Boolean> IsEmailExists(string email);
        Task<ApplicationAccount> GetByUserName(string userName);
    }
}

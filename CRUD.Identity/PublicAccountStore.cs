
using Entity;
using Microsoft.AspNetCore.Identity;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRUD.Identity
{
    public class PublicAccountStore :
         IUserStore<ApplicationAccount>,
         IUserEmailStore<ApplicationAccount>,
         IUserPasswordStore<ApplicationAccount>,
         IUserRoleStore<ApplicationAccount>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;

        public PublicAccountStore(IUnitOfWork unitOfWork,
            IAccountRepository administratorRepository,
            IRoleRepository roleRepository)
        {
            _accountRepository = administratorRepository;
            _roleRepository = roleRepository;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationAccount user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _accountRepository.Add(user);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationAccount user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _accountRepository.Delete(user.AccountId);

            return IdentityResult.Success;
        }

        public Task<ApplicationAccount> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (String.IsNullOrEmpty(normalizedEmail))
            {
                return null;
            }

            return _accountRepository.GetByEmail(normalizedEmail);
        }

        public Task<ApplicationAccount> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (String.IsNullOrEmpty(userId))
            {
                return null;
            }

            return _accountRepository.GetById(Guid.Parse(userId));
        }

        public Task<ApplicationAccount> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (String.IsNullOrEmpty(normalizedUserName))
            {
                return null;
            }

            return _accountRepository.GetByUserName(normalizedUserName);
        }

        public Task<string> GetEmailAsync(ApplicationAccount user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationAccount user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task<string> GetNormalizedEmailAsync(ApplicationAccount user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationAccount user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(ApplicationAccount user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.AccountId.ToString());
        }

        public Task<string> GetUserNameAsync(ApplicationAccount user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetEmailAsync(ApplicationAccount user, string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.Email = email;

            return Task.FromResult(0);
        }

        public Task SetEmailConfirmedAsync(ApplicationAccount user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.EmailConfirmed = confirmed;

            return _accountRepository.Update(user);
        }

        public Task SetNormalizedEmailAsync(ApplicationAccount user, string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.NormalizedEmail = normalizedEmail;

            return Task.FromResult(0);
        }

        public Task SetNormalizedUserNameAsync(ApplicationAccount user, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.NormalizedUserName = normalizedName;

            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(ApplicationAccount user, string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.UserName = userName;

            return Task.FromResult(0);
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationAccount user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _accountRepository.Update(user);

            return IdentityResult.Success;
        }

        public Task SetPasswordHashAsync(ApplicationAccount user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(ApplicationAccount user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationAccount user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public void Dispose()
        {
            // Nothing to Dispose
        }

        public async Task AddToRoleAsync(ApplicationAccount user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _roleRepository.AddToRoleAsync(roleName, user.AccountId, user.AccountId);
        }

        public Task RemoveFromRoleAsync(ApplicationAccount user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationAccount user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _roleRepository.GetRolesAsync(user.AccountId);
        }

        public async Task<bool> IsInRoleAsync(ApplicationAccount user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _roleRepository.IsInRoleAsync(roleName, user.AccountId);
        }

        public Task<IList<ApplicationAccount>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

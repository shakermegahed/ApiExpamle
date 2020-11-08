using AutoMapper;
using CRUD.Identity;
using Domain.Service;
using Entity;
using Entity.DomainModel;
using Microsoft.AspNetCore.Identity;
using Repository;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using static CRUD.Core.Core;

namespace Domain
{
    public class AccountManager : IAccountManager
    {
        #region[vars]
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<ApplicationAccount> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountInfo _accountInfo;
        private readonly IMapper _mapper;
        #endregion

        #region[ctor]
        public AccountManager(
            IAccountRepository accountRepository,
            UserManager<ApplicationAccount> userManager,
            RoleManager<ApplicationRole> roleManager,
            IUnitOfWork unitOfWork,
            IAccountInfo accountInfo,
            IMapper mapper)
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _accountInfo = accountInfo;
            _mapper = mapper;
        }
        #endregion

        #region[actions]
        public async Task<CommandResult<bool>> ChangePassword(string currentPassword, string newPassword)
        {
            try
            {
                var currentAccount = await _userManager.FindByIdAsync(_accountInfo.Id.ToString());
                if (currentAccount == null)
                    return new CommandResult<bool>("User not found");

                var result = await _userManager.ChangePasswordAsync(currentAccount, currentPassword, newPassword);
                if (!result.Succeeded)
                    return new CommandResult<bool>(result.Errors);

                return new CommandResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new CommandResult<bool>(ex.Message);
            }
        }

        public Task<CommandResult<bool>> ConfirmEmailAndSetPassword(SetPasswordModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<CommandResult<Guid>> CreateAccount(AccountModel model)
        {
            try
            {
                var IsAccountEmailExists = await _accountRepository.IsEmailExists(model.Email);
                if (IsAccountEmailExists)
                    return new CommandResult<Guid>("Invalid Account Email, Email is used before");

                model.AccountId = Guid.NewGuid();

                var account = new ApplicationAccount
                {
                    AccountId = model.AccountId.Value,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email,
                    CreatedBy = model.AccountId.Value,
                    CreatedOn = DateTime.UtcNow,
                    Status = AccountStatus.Active,
                    IsDeleted = false,
                };
                await _unitOfWork.StartTransaction().ConfigureAwait(false);
                var createAccountResult = await _userManager.CreateAsync(account, model.Password);
                if (!createAccountResult.Succeeded)
                    return new CommandResult<Guid>(createAccountResult.Errors);

                var isRoleExist = await _roleManager.RoleExistsAsync(model.AccountRole);
                if (!isRoleExist)
                {
                    await _roleManager.CreateAsync(new ApplicationRole { RoleName = model.AccountRole });
                }

                await _userManager.AddToRoleAsync(account, model.AccountRole);

                _unitOfWork.CommitChanges();
                return new CommandResult<Guid>(account.AccountId);

            }
            catch (Exception exception)
            {
                return new CommandResult<Guid>(exception.Message);
            }
        }

        public Task<CommandResult<bool>> DeleteAccount(CommandModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<QuerySingleResult<String>> PublicLogin(LoginModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                {
                    return new QuerySingleResult<String>("Invalid login credentials");
                }
                var account = await _userManager.FindByEmailAsync(model.Email);

                if (account == null || !await _userManager.CheckPasswordAsync(account, model.Password))
                {
                    return new QuerySingleResult<String>("Invalid login credentials");
                }

                if (account.Status == AccountStatus.Inactive)
                {
                    return new QuerySingleResult<String>("Your account is InActive");
                }


                return new QuerySingleResult<String>() { Data = GenerateAuthToken(account) };

            }
            catch (Exception exception)
            {
                // Log.Error("Account login failed. {@Error}", exception.Message);
                return new QuerySingleResult<String>(exception.Message);
            }
        }

        public Task<CommandResult<bool>> ResetPasswordAndClearLockout(SetPasswordModel model)
        {
            throw new NotImplementedException();
        }

        public Task<CommandResult<bool>> RestoreAccount(CommandModel model)
        {
            throw new NotImplementedException();
        }

        public Task<CommandResult<bool>> SendResetPasswordEmail(ForgetPasswordModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<CommandResult<bool>> UpdateAccount(AccountModel model)
        {
            try
            {
                var currentAdministrator = await _userManager.FindByIdAsync(model.AccountId.ToString());
                if (currentAdministrator == null)
                    return new CommandResult<bool>("There is no account founded");

                currentAdministrator.FirstName = model.FirstName;
                currentAdministrator.LastName = model.LastName;
                currentAdministrator.Email = model.Email;
                currentAdministrator.LastModifiedBy = _accountInfo.Id;

                var result = await _userManager.UpdateAsync(currentAdministrator);
                if (!result.Succeeded)
                    return new CommandResult<bool>(result.Errors);

                return new CommandResult<bool>(true);
            }
            catch (Exception exception)
            {
                return new CommandResult<Boolean>(exception.Message);
            }
        }

        #endregion

        #region Helper
        private string GenerateAuthToken(ApplicationAccount account)
        {
            var claims = new List<Claim> {
                    new Claim(CustomClaims.ACCOUNT_EMAIL, account.Email),
                    new Claim(CustomClaims.ACCOUNT_NAME, $"{account.FirstName} {account.LastName}"),
                    new Claim(CustomClaims.ACCOUNT_ID, account.AccountId.ToString()),
                    new Claim(ClaimTypes.Role,account.RoleName),
                };


            return AuthTokenGenerator.GenerateToken(claims);
        }

        #endregion
    }

}

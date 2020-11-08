using Entity;
using Entity.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service
{
    public interface IAccountManager
    {
        Task<CommandResult<Guid>> CreateAccount(AccountModel model);
        Task<CommandResult<Boolean>> UpdateAccount(AccountModel model);
        Task<CommandResult<Boolean>> DeleteAccount(CommandModel model);
        Task<CommandResult<Boolean>> RestoreAccount(CommandModel model);
        Task<QuerySingleResult<String>> PublicLogin(LoginModel model);

        Task<CommandResult<bool>> ConfirmEmailAndSetPassword(SetPasswordModel model);
        Task<CommandResult<bool>> SendResetPasswordEmail(ForgetPasswordModel model);
        Task<CommandResult<bool>> ResetPasswordAndClearLockout(SetPasswordModel model);
        Task<CommandResult<bool>> ChangePassword(string currentPassword, string newPassword);
    }
}

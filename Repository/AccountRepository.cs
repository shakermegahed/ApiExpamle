
using Dapper;
using Entity;
using Entity.DomainModel;
using EntityDatabaseEntities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using static CRUD.Core.Core;
using static System.Data.CommandType;


namespace Repository
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        const string ACCOUNT_INSERT = "[Account].[Insert]";
        const string ACCOUNT_UPDATE = "[Account].[Update]";
        const string ACCOUNT_DELETE = "[Account].[Delete]";
        const string ACCOUNT_GET_BY_ACCOUNT_ID = "[Account].[GetById]";
        const string ACCOUNT_GET_BY_USER_NAME = "[Account].[GetByUserName]";
        const string ACCOUNT_GET_BY_EMAIL = "[Account].[GetByEmail]";
        const string ACCOUNT_LOGIN = "[Account].[Login]";
        const string ACCOUNT_UPDATE_IS_DELETED = "[Account].[UpdateIsDelete]";
        const string ACCOUNT_UPDATE_STATUS = "[Account].[UpdateStatus]";
        const string ACCOUNT_UPDATE_PASSWORD = "[Account].[UpdatePasswordHashed]";
        const string ACCOUNT_EMAIL_EXISTS = "[Account].[EmailIsExists]";
        const string ACCOUNT_GET_ACCOUNTS_BY_ROLE = "[Account].[GetByRoleName]";

        public AccountRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task Add(ApplicationAccount account)
        {
            try
            {
                Connection = await UnitOfWork.GetConnectionAsync();
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@AccountId", account.AccountId);
                parameters.Add("@Email", account.Email);
                parameters.Add("@NormalizedEmail", account.Email.ToUpper());
                parameters.Add("@UserName", account.Email);
                parameters.Add("@NormalizedUserName", account.Email.ToUpper());
                parameters.Add("@EmailConfirmed", false);
                parameters.Add("@PasswordHash", account.PasswordHash);
                parameters.Add("@FirstName", account.FirstName);
                parameters.Add("@LastName", account.LastName);
                parameters.Add("@Status", account.Status);
                parameters.Add("@CreatedOn", DateTime.UtcNow);
                parameters.Add("@CreatedBy", account.CreatedBy);
                parameters.Add("@IsDeleted", account.IsDeleted);
                parameters.Add("@SecurityStamp", null);
                parameters.Add("@LockoutEndDateUtc", null);
                parameters.Add("@LockoutEnabled", false);
                parameters.Add("@AccessFailedCount", 0);

                await SqlMapper.ExecuteAsync(Connection, ACCOUNT_INSERT, parameters, Transaction, commandType: StoredProcedure).ConfigureAwait(false);
            }
            catch (AggregateException aggException)
            {
                //Log.Error("ACCOUNTistrator Add, Error:{@error}, ACCOUNTistrator:{@ACCOUNT}", aggException.Message, ACCOUNT);
                throw new Exception(aggException.Message);
            }
            catch (SqlException sqlException)
            {
                //Log.Error("ACCOUNTistrator Add, Error:{@error}, ACCOUNTistrator:{@ACCOUNT}", sqlException.Message, ACCOUNT);
                throw new Exception(sqlException.Message);
            }
            catch (Exception exception)
            {
                //Log.Error("ACCOUNTistrator Add, Error:{@error}, ACCOUNTistrator:{@ACCOUNT}", exception.Message, ACCOUNT);
                throw exception;
            }
        }

        public async Task Delete(Guid accountId)
        {
            try
            {
                Connection = await UnitOfWork.GetConnectionAsync();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ACCOUNTistratorId", accountId);

                await SqlMapper.ExecuteAsync(Connection, ACCOUNT_DELETE, parameters, Transaction, commandType: StoredProcedure).ConfigureAwait(false);
            }
            catch (AggregateException aggException)
            {
                //Log.Error("ACCOUNTistrator Delete, Error:{@error}, ACCOUNTistrator:{@ACCOUNTId}", aggException.Message, ACCOUNTId);
                throw new Exception(aggException.Message);
            }
            catch (SqlException sqlException)
            {
                //Log.Error("ACCOUNTistrator Delete, Error:{@error}, ACCOUNTistrator:{@ACCOUNTId}", sqlException.Message, ACCOUNTId);
                throw new Exception(sqlException.Message);
            }
            catch (Exception exception)
            {
                //Log.Error("ACCOUNTistrator Delete, Error:{@error}, ACCOUNTistrator:{@ACCOUNTId}", exception.Message, ACCOUNTId);
                throw exception;
            }
        }

        public async Task<IEnumerable<AccountModel>> GetAccountByRoleName(string roleName, string SearchKeyWord, PagedQueryParameters pagedQueryParameters)
        {
            try
            {
                Connection = await UnitOfWork.GetConnectionAsync();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@RoleName", roleName);
                parameters.Add("@SearchKeyWord", SearchKeyWord);
                parameters.Add("@PageSize", pagedQueryParameters.PageSize);
                parameters.Add("@PageNumber", pagedQueryParameters.PageNumber);

                return await SqlMapper.QueryAsync<AccountModel>(Connection, ACCOUNT_GET_ACCOUNTS_BY_ROLE, parameters, Transaction, commandType: StoredProcedure).ConfigureAwait(false);
            }
            catch (AggregateException aggException)
            {
                throw new Exception(aggException.Message);
            }
            catch (SqlException sqlException)
            {
                throw new Exception(sqlException.Message);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<ApplicationAccount> GetByEmail(string email)
        {
            try
            {
                Connection = await UnitOfWork.GetConnectionAsync();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Email", email);
                parameters.Add("@TodayDate", DateTime.UtcNow.Date);

                var data = await SqlMapper.QueryMultipleAsync(Connection, ACCOUNT_GET_BY_EMAIL, parameters, Transaction, commandType: StoredProcedure).ConfigureAwait(false);
                // 0 - account Id

                // 1 - ACCOUNTistrator
                var ACCOUNT = data.ReadFirstOrDefault<ApplicationAccount>();



                return ACCOUNT;
            }
            catch (AggregateException aggException)
            {
                //Log.Error("ACCOUNTistrator GetAccountACCOUNTistratorByEmail, Error:{@error}, Email:{@email}", aggException.Message, email);
                throw new Exception(aggException.Message);
            }
            catch (SqlException sqlException)
            {
                //Log.Error("ACCOUNTistrator GetAccountACCOUNTistratorByEmail, Error:{@error}, Email:{@email}", sqlException.Message, email);
                throw new Exception(sqlException.Message);
            }
            catch (Exception exception)
            {
                //Log.Error("ACCOUNTistrator GetAccountACCOUNTistratorByEmail, Error:{@error}, Email:{@email}", exception.Message, email);
                throw exception;
            }
        }

        public async Task<ApplicationAccount> GetById(Guid accountId)
        {
            try
            {
                Connection = await UnitOfWork.GetConnectionAsync();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@accountId", accountId);

                return await SqlMapper.QuerySingleOrDefaultAsync<ApplicationAccount>(Connection, ACCOUNT_GET_BY_ACCOUNT_ID, parameters, Transaction, commandType: StoredProcedure).ConfigureAwait(false);
            }
            catch (AggregateException aggException)
            {
                //Log.Error("ACCOUNTistrator GetById, Error:{@error}, ACCOUNTistratorId:{@ACCOUNTistratorId}", aggException.Message, ACCOUNTistratorId);
                throw new Exception(aggException.Message);
            }
            catch (SqlException sqlException)
            {
                //Log.Error("ACCOUNTistrator GetById, Error:{@error}, ACCOUNTistratorId:{@ACCOUNTistratorId}", sqlException.Message, ACCOUNTistratorId);
                throw new Exception(sqlException.Message);
            }
            catch (Exception exception)
            {
                //Log.Error("ACCOUNTistrator GetById, Error:{@error}, ACCOUNTistratorId:{@ACCOUNTistratorId}", exception.Message, ACCOUNTistratorId);
                throw exception;
            }
        }

        public async Task<ApplicationAccount> GetByUserName(string userName)
        {
            try
            {
                Connection = await UnitOfWork.GetConnectionAsync();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserName", userName);

                return await SqlMapper.QuerySingleOrDefaultAsync<ApplicationAccount>(Connection, ACCOUNT_GET_BY_USER_NAME, parameters, Transaction, commandType: StoredProcedure).ConfigureAwait(false);
            }
            catch (AggregateException aggException)
            {
                //Log.Error("ACCOUNTistrator GetByUserName, Error:{@error}, ACCOUNTistrator:{@userName}", aggException.Message, userName);
                throw new Exception(aggException.Message);
            }
            catch (SqlException sqlException)
            {
                //Log.Error("ACCOUNTistrator GetByUserName, Error:{@error}, ACCOUNTistrator:{@userName}", sqlException.Message, userName);
                throw new Exception(sqlException.Message);
            }
            catch (Exception exception)
            {
                //Log.Error("ACCOUNTistrator GetByUserName, Error:{@error}, ACCOUNTistrator:{@userName}", exception.Message, userName);
                throw exception;
            }
        }

        public async Task<bool> IsEmailExists(string email)
        {

            try
            {
                Connection = await UnitOfWork.GetConnectionAsync();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Email", email);

                return await SqlMapper.QuerySingleAsync<bool>(Connection, ACCOUNT_EMAIL_EXISTS, parameters, Transaction, commandType: StoredProcedure).ConfigureAwait(false);
            }
            catch (AggregateException aggException)
            {
                //Log.Error("Account IsAccountEmailExists, Error:{@error}, Email:{@accountName}", aggException.Message, email);
                throw new Exception(aggException.Message);
            }
            catch (SqlException sqlException)
            {
                //Log.Error("Account IsAccountEmailExists, Error:{@error}, Email:{@accountName}", sqlException.Message, email);
                throw new Exception(sqlException.Message);
            }
            catch (Exception exception)
            {
                //Log.Error("Account IsAccountEmailExists, Error:{@error}, Email:{@accountName}", exception.Message, email);
                throw exception;
            }


        }

        public async Task<AccountEntity> Login(string email, string passwordHashed)
        {
            try
            {
                Connection = await UnitOfWork.GetConnectionAsync();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Email", email);
                parameters.Add("@PasswordHash", passwordHashed);

                return await SqlMapper.QuerySingleAsync<AccountEntity>(Connection, ACCOUNT_LOGIN, parameters, Transaction, commandType: StoredProcedure).ConfigureAwait(false);
            }
            catch (AggregateException aggException)
            {
                //Log.Error("ACCOUNTistrator Login, Error:{@error}, ACCOUNTistrator:{@ACCOUNTEmail}, ACCOUNTistratorPassword:{@passwordHashed}", aggException.Message, ACCOUNTEmail, passwordHashed);
                throw new Exception(aggException.Message);
            }
            catch (SqlException sqlException)
            {
                //Log.Error("ACCOUNTistrator Login, Error:{@error}, ACCOUNTistrator:{@ACCOUNTEmail}, ACCOUNTistratorPassword:{@passwordHashed}", sqlException.Message, ACCOUNTEmail, passwordHashed);
                throw new Exception(sqlException.Message);
            }
            catch (Exception exception)
            {
                //Log.Error("ACCOUNTistrator Login, Error:{@error}, ACCOUNTistrator:{@ACCOUNTEmail}, ACCOUNTistratorPassword:{@passwordHashed}", exception.Message, ACCOUNTEmail, passwordHashed);
                throw exception;
            }
        }

        public async Task Update(ApplicationAccount account)
        {
            try
            {
                Connection = await UnitOfWork.GetConnectionAsync();
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@AccountId", account.AccountId);
                parameters.Add("@Email", account.Email);
                parameters.Add("@NormalizedEmail", account.Email.ToUpper());
                parameters.Add("@UserName", account.Email);
                parameters.Add("@NormalizedUserName", account.Email.ToUpper());
                parameters.Add("@EmailConfirmed", account.EmailConfirmed);
                parameters.Add("@PasswordHash", account.PasswordHash);
                parameters.Add("@FirstName", account.FirstName);
                parameters.Add("@LastName", account.LastName);
                parameters.Add("@Status", account.Status);
                parameters.Add("@LastModifiedOn", DateTime.UtcNow);
                parameters.Add("@IsDeleted", account.IsDeleted);
                parameters.Add("@SecurityStamp", null);
                parameters.Add("@LockoutEndDateUtc", null);
                parameters.Add("@LockoutEnabled", false);
                parameters.Add("@AccessFailedCount", 0);

                await SqlMapper.ExecuteAsync(Connection, ACCOUNT_UPDATE, parameters, Transaction, commandType: StoredProcedure).ConfigureAwait(false);
            }
            catch (AggregateException aggException)
            {
                //Log.Error("ACCOUNTistrator Update, Error:{@error}, ACCOUNTistrator:{@ACCOUNTistrator}", aggException.Message, account);
                throw new Exception(aggException.Message);
            }
            catch (SqlException sqlException)
            {
                //Log.Error("ACCOUNTistrator Update, Error:{@error}, ACCOUNTistrator:{@ACCOUNTistrator}", sqlException.Message, ACCOUNT);
                throw new Exception(sqlException.Message);
            }
            catch (Exception exception)
            {
                //Log.Error("ACCOUNTistrator Update, Error:{@error}, ACCOUNTistrator:{@ACCOUNTistrator}", exception.Message, ACCOUNT);
                throw exception;
            }
        }

        public async Task UpdateIsDeleted(AccountEntity account)
        {
            try
            {
                Connection = await UnitOfWork.GetConnectionAsync();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AccountId", account.AccountId);
                parameters.Add("@IsDeleted", account.IsDeleted);
                parameters.Add("@LastModifiedOn", DateTime.UtcNow);
                parameters.Add("@LastModifiedBy", account.LastModifiedBy);


                await SqlMapper.ExecuteAsync(Connection, ACCOUNT_UPDATE_IS_DELETED, parameters, Transaction, commandType: StoredProcedure).ConfigureAwait(false);
            }
            catch (AggregateException aggException)
            {
                //Log.Error("Administrator UpdateIsDeleted, Error:{@error}, Administrator:{@administratorId}", aggException.Message, admin);
                throw new Exception(aggException.Message);
            }
            catch (SqlException sqlException)
            {
                //Log.Error("Administrator UpdateIsDeleted, Error:{@error}, Administrator:{@administratorId}", sqlException.Message, admin);
                throw new Exception(sqlException.Message);
            }
            catch (Exception exception)
            {
                //Log.Error("Administrator UpdateIsDeleted, Error:{@error}, Administrator:{@administratorId}", exception.Message, admin);
                throw exception;
            }
        }

        public async Task UpdateStatus(Guid accountId, AccountStatus status)
        {
            try
            {
                Connection = await UnitOfWork.GetConnectionAsync();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AccountId", accountId);
                parameters.Add("@Status", status);
                parameters.Add("@LastModifiedOn", DateTime.UtcNow);

                await SqlMapper.ExecuteAsync(Connection, ACCOUNT_UPDATE_STATUS, parameters, Transaction, commandType: StoredProcedure).ConfigureAwait(false);
            }
            catch (AggregateException aggException)
            {
                //Log.Error("Administrator UpdateStatus, Error:{@error}, Administrator:{@adminId}, Status:{@status}", aggException.Message, accountId, status);
                throw new Exception(aggException.Message);
            }
            catch (SqlException sqlException)
            {
                //Log.Error("Administrator UpdateStatus, Error:{@error}, Administrator:{@adminId}, Status:{@status}", sqlException.Message, accountId, status);
                throw new Exception(sqlException.Message);
            }
            catch (Exception exception)
            {
                //Log.Error("Administrator UpdateStatus, Error:{@error}, Administrator:{@adminId}, Status:{@status}", exception.Message, adminId, status);
                throw exception;
            }
        }
    }
}

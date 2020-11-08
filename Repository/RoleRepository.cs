using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Entity;
using static System.Data.CommandType;


namespace Repository
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public RoleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task Add(ApplicationRole Role)
        {
            try
            {
                Connection = await UnitOfWork.GetConnectionAsync();

                var SQL = $"INSERT INTO [AUTH].[ROLE] (RoleId, RoleName, NormalizedRoleName) VALUES ('{Role.RoleId}', '{Role.RoleName}', '{Role.NormalizedRoleName}')";

                await SqlMapper.ExecuteAsync(Connection, SQL, transaction: Transaction, commandType: Text).ConfigureAwait(false);
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

        public async Task AddToRoleAsync(string RoleName, Guid AccountId, Guid? CreatedBy = null)
        {
            try
            {
                Connection = await UnitOfWork.GetConnectionAsync();
                var strCreatedBy = CreatedBy.HasValue ? $"'{CreatedBy.Value.ToString()}'" : "NULL";

                var SQL = $@"DECLARE @RoleId UNIQUEIDENTIFIER; 
                             SELECT @RoleId = RoleId FROM [AUTH].[ROLE] 
                             WHERE RoleName = '{RoleName}';

                            INSERT INTO [AUTH].[AccountRole] (RoleId, AccountId, CreatedOn, CreatedBy) 
                                    VALUES ( @RoleId, '{AccountId}' ,'{DateTime.UtcNow}', {strCreatedBy})";

                await SqlMapper.ExecuteAsync(Connection, SQL, transaction: Transaction, commandType: Text).ConfigureAwait(false);
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

        public async Task Delete(Guid RoleId)
        {
            try
            {
                Connection = await UnitOfWork.GetConnectionAsync();

                var SQL = $"DELETE FROM [AUTH].[ROLE] WHERE RoleId = '{RoleId}'";

                await SqlMapper.ExecuteAsync(Connection, SQL, transaction: Transaction, commandType: Text).ConfigureAwait(false);
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

        public async Task<ApplicationRole> GetById(Guid RoleId)
        {
            try
            {
                Connection = await UnitOfWork.GetConnectionAsync();

                var SQL = $"SELECT * FROM [AUTH].[ROLE] WHERE RoleId = '{RoleId}'";

                return await SqlMapper.QuerySingleOrDefaultAsync<ApplicationRole>(Connection, SQL, transaction: Transaction, commandType: Text).ConfigureAwait(false);
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

        public async Task<ApplicationRole> GetByRoleName(string RoleName)
        {
            try
            {
                Connection = await UnitOfWork.GetConnectionAsync();

                var SQL = $"SELECT * FROM [AUTH].[ROLE] WHERE RoleName = '{RoleName}'";

                return await SqlMapper.QuerySingleOrDefaultAsync<ApplicationRole>(Connection, SQL, transaction: Transaction, commandType: Text).ConfigureAwait(false);
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

        public async Task<IList<string>> GetRolesAsync(Guid AccountId)
        {
            try
            {
                Connection = await UnitOfWork.GetConnectionAsync();

                var SQL = $"SELECT RoleName FROM [AUTH].[ROLE] WHERE AccountId = '{AccountId}'";

                var roles = await SqlMapper.QueryAsync<string>(Connection, SQL, transaction: Transaction, commandType: Text).ConfigureAwait(false);

                return roles.ToList();
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

        public async Task<bool> IsInRoleAsync(string RoleName, Guid AccountId)
        { 
            try
            {
                Connection = await UnitOfWork.GetConnectionAsync();

                var SQL = $"SELECT COUNT(*) FROM [AUTH].[ROLE] role INNER JOIN [AUTH].[AccountRole] adminrole ON role.RoleId = adminrole.RoleId " +
                    $"WHERE AccountId = '{AccountId}' AND role.RoleName = '{RoleName}'";

                var count = await SqlMapper.QuerySingleAsync<int>(Connection, SQL, transaction: Transaction, commandType: Text).ConfigureAwait(false);

                return count > 0;
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

        public async Task Update(ApplicationRole Role)
        {
            try
            {
                Connection = await UnitOfWork.GetConnectionAsync();

                var SQL = $"UPDATE [AUTH].[ROLE] SET RoleName = '{Role.RoleName}', NormalizedRoleName = '{Role.NormalizedRoleName}' WHERE RoleId = '{Role.RoleId}'";

                await SqlMapper.ExecuteAsync(Connection, SQL, transaction: Transaction, commandType: Text).ConfigureAwait(false);
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
    }
}


using Entity;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private SqlConnection connection;
        private SqlTransaction transaction;
        private IOptions<ConnectionStrings> config;

        public UnitOfWork(IOptions<ConnectionStrings> _config)
        {
            this.config = _config;
        }

        public async Task<SqlConnection> GetConnectionAsync()
        {
            if (connection != null && connection.State != ConnectionState.Closed)
            {
                return connection;
            }

            string connectionString = config.Value.DefaultConnection;

            connection = new SqlConnection(connectionString);
            await connection.OpenAsync().ConfigureAwait(false);
            return connection;
        }

        public SqlTransaction GetTransaction()
        {
            return this.transaction;
        }

        public async Task StartTransaction()
        {
            await this.GetConnectionAsync().ConfigureAwait(false);
            this.transaction = this.connection.BeginTransaction();

            // this.transaction.IsolationLevel = IsolationLevel.ReadUncommitted;
        }

        public void CommitChanges()
        {
            try
            {
                this.transaction.Commit();
            }
            catch (Exception exception)
            {
                this.transaction.Rollback();
                throw new Exception("Transaction Commit Failed", exception);
            }
            finally
            {
                this.transaction.Dispose();
                this.connection.Close();
            }
        }

        public void Rollback()
        {
            try
            {
                this.transaction.Rollback();
            }
            catch (Exception exception)
            {
                throw new Exception("Transaction Rollback Failed", exception);
            }
            finally
            {
                this.transaction.Dispose();
                this.connection.Close();
            }
        }

        public void Dispose()
        {
            if (this.transaction != null) this.transaction.Dispose();
            if (this.connection != null) this.connection.Close();
        }

    }
}

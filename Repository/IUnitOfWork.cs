using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUnitOfWork
    {
        Task<SqlConnection> GetConnectionAsync();

        SqlTransaction GetTransaction();

        void CommitChanges();

        void Rollback();

        Task StartTransaction();
    }
}

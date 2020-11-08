using System;
using System.Data.SqlClient;

namespace Repository
{
    public class BaseRepository : IDisposable
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public SqlConnection Connection { get; set; }

        public SqlTransaction Transaction
        {
            get
            {
                return UnitOfWork.GetTransaction();
            }
        }

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public void Dispose()
        {

        }
    }
}

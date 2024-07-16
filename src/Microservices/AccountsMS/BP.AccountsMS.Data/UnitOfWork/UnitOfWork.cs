using System.Data;
using BP.AccountsMS.Data.Repositories;
using BP.AccountsMS.Data.Repositories.Contracts;
using BP.AccountsMS.Data.UnitOfWork.Contracts;

namespace BP.AccountsMS.Data.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IDbTransaction _transaction;
        private readonly IDbConnection _connection;

        private IAccountRepository _userRepository;
        private bool _isDisposed = false;

        public UnitOfWork(IDbConnection dbConnection)
        {
            _connection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IAccountRepository AccountRepository
        {
            get
            {
                return _userRepository ??= new AccountRepository(_transaction);
            }
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _transaction?.Connection?.Close();
                    _transaction?.Connection?.Dispose();
                    _transaction?.Dispose();
                }

                _isDisposed = true;
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}

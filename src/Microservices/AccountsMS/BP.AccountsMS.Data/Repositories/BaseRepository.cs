using System.Data;

namespace BP.AccountsMS.Data.Repositories
{
    internal abstract class BaseRepository
    {
        protected readonly IDbTransaction transaction;
        protected IDbConnection connection => transaction.Connection;

        public BaseRepository(IDbTransaction transaction)
        {
            this.transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }
    }
}

using System.Data;
using BP.AccountsMS.Data.Entities;
using BP.AccountsMS.Data.Repositories.Contracts;
using Dapper;

namespace BP.AccountsMS.Data.Repositories
{
    internal class OneTimePasswordRepository : BaseRepository, IOneTimePasswordRepository
    {
        public OneTimePasswordRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<OneTimePasswordEntity> GetActiveAsync(Guid accountId)
        {
            var sql = $@"SELECT * FROM OneTimePasswords WHERE IsActive = 1 AND UserId = @AccountId";

            var entities = await connection.QueryAsync<OneTimePasswordEntity>(
                sql,
                param: new { AccountId = accountId },
                transaction: transaction);

            return entities.FirstOrDefault();
        }

        public async Task<Guid> CreateAsync(OneTimePasswordEntity oneTimePasswordEntity)
        {
            var sql = $@"INSERT INTO OneTimePasswords 
                VALUES(@Id, @UserId, @Password, @CreatedAtUtc, @ExpiredAtUtc, @IsActive)";

            await connection.ExecuteAsync(
                sql,
                param: new
                {
                    Id = oneTimePasswordEntity.Id,
                    UserId = oneTimePasswordEntity.UserId,
                    Password = oneTimePasswordEntity.Password,
                    CreatedAtUtc = oneTimePasswordEntity.CreatedAtUtc,
                    ExpiredAtUtc = oneTimePasswordEntity.ExpiredAtUtc,
                    IsActive = oneTimePasswordEntity.IsActive
                },
                transaction: transaction);

            return oneTimePasswordEntity.Id;
        }

        public async Task DeactivateOneAsync(Guid passwordId)
        {
            var sql = $@"UPDATE OneTimePasswords
                SET IsActive = 0, ExpiredAtUtc = @ExpiredAtUtc
                WHERE Id = @Id";

            await connection.ExecuteAsync(
                sql,
                param: new
                {
                    ExpiredAtUtc = DateTime.UtcNow,
                    Id = passwordId 
                },
                transaction: transaction);
        }

        public async Task DeactivateAllAsync(Guid accountId)
        {
            var sql = $@"UPDATE OneTimePasswords
                SET IsActive = 0, ExpiredAtUtc = @ExpiredAtUtc
                WHERE IsActive = 1 AND UserId = @AccountId";

            await connection.ExecuteAsync(
                sql,
                param: new 
                {
                    ExpiredAtUtc = DateTime.UtcNow,
                    AccountId = accountId 
                },
                transaction: transaction);
        }
    }
}

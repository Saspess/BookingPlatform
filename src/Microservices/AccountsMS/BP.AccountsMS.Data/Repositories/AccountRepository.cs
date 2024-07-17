using System.Data;
using BP.AccountsMS.Data.Entities;
using BP.AccountsMS.Data.Repositories.Contracts;
using Dapper;

namespace BP.AccountsMS.Data.Repositories
{
    internal class AccountRepository : BaseRepository, IAccountRepository
    {
        public AccountRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<AccountEntity> GetByIdAsync(Guid id)
        {
            var sql = $@"SELECT * FROM Users WHERE Id = @Id";

            var entities = await connection.QueryAsync<AccountEntity>(
                sql,
                param: new { Id = id },
                transaction: transaction);

            return entities.FirstOrDefault();
        }

        public async Task<AccountEntity> GetByEmailAsync(string email)
        {
            var sql = $@"SELECT * FROM Users WHERE Email = @Email";

            var entities = await connection.QueryAsync<AccountEntity>(
                sql,
                param: new { Email = email },
                transaction: transaction);

            return entities.FirstOrDefault();
        }

        public async Task<Guid> CreateAsync(AccountEntity accountEntity)
        {
            var sql = $@"INSERT INTO Users 
                VALUES(@Id, @FirstName, @LastName, @Email, @IsEmailConfirmed, @Role)";

            await connection.ExecuteAsync(
                sql,
                param: new
                {
                    Id = accountEntity.Id,
                    FirstName = accountEntity.FirstName,
                    LastName = accountEntity.LastName,
                    Email = accountEntity.Email,
                    IsEmailConfirmed = accountEntity.IsEmailConfirmed,
                    Role = accountEntity.Role
                },
                transaction: transaction);

            return accountEntity.Id;
        }

        public async Task UpdateAsync(AccountEntity accountEntity)
        {
            var sql = $@"UPDATE Users
                SET FirstName = @FirstName, LastName = @LastName, Email = @Email, IsEmailConfirmed = @IsEmailConfirmed
                WHERE Id = @Id";

            await connection.ExecuteAsync(
                sql,
                param: new
                {
                    FirstName = accountEntity.FirstName,
                    LastName = accountEntity.LastName,
                    Email = accountEntity.Email,
                    IsEmailConfirmed = accountEntity.IsEmailConfirmed,
                    Id = accountEntity.Id
                },
                transaction: transaction);
        }
    }
}

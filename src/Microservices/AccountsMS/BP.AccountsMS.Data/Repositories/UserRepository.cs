using System.Data;
using BP.AccountsMS.Data.Entities;
using BP.AccountsMS.Data.Repositories.Contracts;
using Dapper;

namespace BP.AccountsMS.Data.Repositories
{
    internal class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<UserEntity> GetByEmailAsync(string email)
        {
            var sql = $@"SELECT * FROM Users WHERE Email = @Email";

            var entities = await connection.QueryAsync<UserEntity>(
                sql,
                param: new { Email = email },
                transaction: transaction
                );

            return entities.FirstOrDefault();
        }

        public async Task<Guid> CreateAsync(UserEntity userEntity)
        {
            var sql = $@"INSERT INTO Users 
                VALUES(@Id, @FirstName, @LastName, @Email, @IsEmailConfirmed, @Role)";

            await connection.ExecuteAsync(sql,
                param: new
                {
                    Id = userEntity.Id,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    Email = userEntity.Email,
                    IsEmailConfirmed = userEntity.IsEmailConfirmed,
                    Role = userEntity.Role
                },
                transaction: transaction);

            return userEntity.Id;
        }
    }
}

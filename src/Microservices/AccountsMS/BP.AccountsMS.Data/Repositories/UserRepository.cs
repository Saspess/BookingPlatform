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

        public async Task<Guid> CreateUserAsync(UserEntity userEntity)
        {
            var sqlQuery = $@"INSERT INTO Users 
                VALUES(@Id, @FirstName, @LastName, @Email, @IsEmailConfirmed)";

            await connection.ExecuteAsync(sqlQuery,
                param: new
                {
                    Id = userEntity.Id,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    Email = userEntity.Email,
                    IsEmailConfirmed = userEntity.IsEmailConfirmed
                },
                transaction: transaction);

            return userEntity.Id;
        }
    }
}

using System.Data.SqlClient;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.Users;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookStore.DL.Repositories.MsSQL
{
    public class UserStore : IUserRepo, IUserPasswordStore<User>
    {
        private readonly ILogger<UserStore> _logger;
        private readonly IConfiguration _configuration;
        public UserStore(ILogger<UserStore> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    string query =
                        @"INSERT INTO UserInfo 
                        ([UserId]
                       ,[DisplayName]
                       ,[UserName]
                       ,[Email]
                       ,[Password]
                       ,[GreatedDate])
                        VALUES 
                       (@UserId
                       ,@DisplayName
                       ,@UserName
                       ,@Email
                       ,@Password
                       ,@GreatedDate)";
                    await conn.QueryAsync<Employee>(query, user);
                    return IdentityResult.Success;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(CreateAsync)} : {e.Message}");
            }
            return IdentityResult.Failed();
        }
        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = "SELECT * FROM UserInfo WITH(NOLOCK) WHERE UserName = @name";
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<User>(query, new { name = normalizedUserName });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(FindByNameAsync)} : {e.Message}");
            }

            return null;
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName.Normalize()); 
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserId.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public async Task<User> GetUsersInfo(string _email, string _password)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = "SELECT * FROM UserInfo WITH(NOLOCK) WHERE Email = @email AND Password = @password";
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<User>(query, new { email = _email, password = _password });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetUsersInfo)} : {e.Message}");
            }

            return null;
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

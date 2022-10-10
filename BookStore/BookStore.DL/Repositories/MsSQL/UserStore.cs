using System.Data.SqlClient;
using BookStore.Models.Models.Users;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookStore.DL.Repositories.MsSQL
{
    public class UserStore : IUserPasswordStore<User>,IUserRoleStore<User>
    {
        private readonly ILogger<UserStore> _logger;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserStore(ILogger<UserStore> logger, IConfiguration configuration, IPasswordHasher<User> passwordHasher)
        {
            _logger = logger;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
                        ([DisplayName]
                       ,[UserName]
                       ,[Email]
                       ,[Password]
                       ,[CreatedDate])
                        VALUES 
                       (@DisplayName
                       ,@UserName
                       ,@Email
                       ,@Password
                       ,@CreatedDate)";

                    user.Password = _passwordHasher.HashPassword(user, user.Password);

                    var result = await conn.QueryAsync<Employee>(query, user);

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
                    await conn.OpenAsync(cancellationToken);

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

        public async Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {

            await using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await conn.OpenAsync(cancellationToken);

            return await conn.QueryFirstOrDefaultAsync<string>("SELECT Password FROM UserInfo WITH(NOLOCK) WHERE UserId = @id", new { id = user.UserId });
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = "SELECT r.RoleName FROM Roles r WHERE r.Id IN (SELECT ur.Id FROM UserRoles ur WHERE ur.UserId = @userId)";
                    await conn.OpenAsync(cancellationToken);

                    var result = await conn.QueryAsync<string>(query, new { userId = user.UserId });
                    return result.ToList();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(FindByNameAsync)} : {e.Message}");
            }

            return null;
        }

        public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = "SELECT * FROM UserInfo WITH(NOLOCK) WHERE UserId = @id";
                    await conn.OpenAsync(cancellationToken);

                    var result = await conn.QueryFirstOrDefaultAsync<User>(query, new { id = user.UserId });
                    return result?.UserId.ToString();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(FindByNameAsync)} : {e.Message}");
            }

            return null;
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

        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return !string.IsNullOrEmpty(await GetPasswordHashAsync(user, cancellationToken));
        }

        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            await using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await conn.OpenAsync(cancellationToken);

            await conn.ExecuteAsync("UPDATE UserInfo SET Passward = @passHash WHERE UserId = @id",new {id = user.UserId, passHash = passwordHash});
        }

        public async Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}

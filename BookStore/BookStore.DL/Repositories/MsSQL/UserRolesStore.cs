using System.Data;
using System.Data.SqlClient;
using BookStore.Models.Models.Users;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookStore.DL.Repositories.MsSQL
{
    public class UserRolesStore : IRoleStore<UserRole>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserRolesStore> _logger;
        public UserRolesStore(IConfiguration configuration, ILogger<UserRolesStore> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<IdentityResult> CreateAsync(UserRole role, CancellationToken cancellationToken)
        {
            await using (var conn = new SqlConnection(_configuration.GetConnectionString("Default Connection")))
            {
                try
                {
                    await conn.OpenAsync(cancellationToken);

                    var query =
                    @"INSERT INTO UserRoles
                     ([Id],
                      [RoleId],
                      [UserId])
                     VALUES
                     (@Id,
                      @RoleId,
                      @UserId)
                    ";

                    var result = await conn.ExecuteAsync(query, role);

                    return IdentityResult.Success;
                }
                catch (Exception e)
                {
                    _logger.LogError($"Error:{e.Message}");
                    return IdentityResult.Failed();
                }

            }
        }

        public Task<IdentityResult> DeleteAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<UserRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<UserRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            await using (var conn = new SqlConnection(_configuration.GetConnectionString("Default Connection")))
            {
                try
                {
                    await conn.OpenAsync(cancellationToken);

                    var query =
                    @"
                        SELECT ur.Id,ur.RoleId,ur.UserId,r.RoleName as [Name] FROM UserRoles ur WITH (NOLOCK)
                        INNER JOIN Roles r ON ur.RoleId = r.Id
                        WHERE r.RoleName = @normalizedRoleName
                    ";

                    var result = await conn.QueryFirstOrDefaultAsync<UserRole>(query, new { normalizedRoleName = normalizedRoleName });

                    return result;
                }
                catch (Exception e)
                {
                    _logger.LogError($"Error:{e.Message}");
                    return null;
                }

            }
        }

        public Task<string> GetNormalizedRoleNameAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleNameAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedRoleNameAsync(UserRole role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleNameAsync(UserRole role, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

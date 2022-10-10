using System.Data.SqlClient;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.Users;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookStore.DL.Repositories.MsSQL
{
    public class EmployeeRepository : IEmployeeRepo
    {
        private readonly ILogger<EmployeeRepository> _logger;
        private readonly IConfiguration _configuration;
        public EmployeeRepository(ILogger<EmployeeRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    string query =
                        @"INSERT INTO Employee 
                        ([EmployeeID]
                       ,[NationalIDNumber]
                       ,[EmployeeName]
                       ,[LoginID]
                       ,[JobTitle]
                       ,[BirthDate]
                       ,[MaritalStatus]
                       ,[Gender]
                       ,[HireDate]
                       ,[VacationHours]
                       ,[SickLeaveHours]
                       ,[rowguid]
                       ,[ModifiedDate])
                        VALUES 
                       (@EmployeeID
                       ,@NationalIDNumber
                       ,@EmployeeName
                       ,@LoginID
                       ,@JobTitle
                       ,@BirthDate
                       ,@MaritalStatus
                       ,@Gender
                       ,@HireDate
                       ,@VacationHours
                       ,@SickLeaveHours
                       ,@rowguid
                       ,@ModifiedDate)";
                    await conn.QueryAsync<Employee>(query, employee );
                    return employee;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(AddEmployee)} : {e.Message}");
            }
            return null;
        }

        public Task<bool> CheckEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> DeleteEmployee(int _id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var deletedUser = await GetByID(_id);
                    var query = "DELETE FROM Employee WHERE EmployeeID = @id";
                    await conn.OpenAsync();

                    await conn.QueryAsync<Employee>(query,new { id = _id });
                    return deletedUser;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(DeleteEmployee)} : {e.Message}");
            }

            return null;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = "SELECT * FROM Employee WITH(NOLOCK)";
                    await conn.OpenAsync();

                    return await conn.QueryAsync<Employee>(query);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllEmployees)} : {e.Message}");
            }

            return Enumerable.Empty<Employee>();
        }

        public async Task<Employee> GetByID(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = "SELECT * FROM Employee WITH(NOLOCK) WHERE EmployeeID = @Id";
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Employee>(query, new { Id = id });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetByID)} : {e.Message}");
            }

            return null;
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    string query =
                      @"UPDATE [dbo].[Employee]
                       SET [EmployeeID] = @EmployeeID
                          ,[NationalIDNumber] = @NationalIDNumber
                          ,[EmployeeName] = @EmployeeName
                          ,[LoginID] = @LoginID
                          ,[JobTitle] = @JobTitle
                          ,[BirthDate] = @BirthDate
                          ,[MaritalStatus] = @MaritalStatus
                          ,[Gender] = @Gender
                          ,[HireDate] = @HireDate
                          ,[VacationHours] = @VacationHours
                          ,[SickLeaveHours] = @SickLeaveHours
                          ,[rowguid] = @rowguid
                          ,[ModifiedDate] = @ModifiedDate
                     WHERE EmployeeID = @EmployeeID";

                    var result = await conn.ExecuteAsync(query, employee);
                    return employee;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(AddEmployee)} : {e.Message}");
            }
            return null;
        }
    }
}

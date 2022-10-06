using System;
using System.Data.SqlClient;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookStore.DL.Repositories.MsSQL
{
    public class AuthorRepository : IAuthorRepo
    {
        private readonly ILogger<AuthorRepository> _logger;
        private readonly IConfiguration _configuration;
        public AuthorRepository(ILogger<AuthorRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<Author> AddAuthor(Author author)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    await conn.QueryAsync<Author>("INSERT INTO Authors VALUES (@Name, @Age, @DateOfBirth, @NickName)",
                        new { Name = author.Name, Age = author.Age, DateOfBirth = author.DateOfBirth, NickName = author.Nickname });
                    return await GetAuthorByName(author.Name);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(AddAuthor)} : {e.Message}");
            }
            return null;
        }
        public async Task<Author> DeleteAuthor(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var deletedUser = await GetByID(id);
                    var query = $"DELETE FROM Authors WHERE Id = {id}";
                    await conn.OpenAsync();

                    await conn.QueryAsync<Author>(query);
                    return deletedUser;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(DeleteAuthor)} : {e.Message}");
            }

            return null;
        }
        public async Task<IEnumerable<Author>> GetAll()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = "SELECT * FROM Authors WITH(NOLOCK)";
                    await conn.OpenAsync();

                    return await conn.QueryAsync<Author>(query);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAll)} : {e.Message}");
            }

            return Enumerable.Empty<Author>();
        }

        public async Task<Author> GetAuthorByName(string authorName)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = "SELECT * FROM Authors WITH(NOLOCK) WHERE Name = @Name";
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Author>(query, new { Name = authorName });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAuthorByName)} : {e.Message}");
            }

            return null;
        }

        public async Task<Author> GetByID(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = "SELECT * FROM Authors WITH(NOLOCK) WHERE Id = @Id";
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Author>(query, new {Id = id});
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetByID)} : {e.Message}");
            }

            return null;
        }
        public async Task<Author> UpdateAuthor(Author person)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = "UPDATE Authors SET Name = @Name, Age = @Age, NickName = @NickName, DateOfBirth = @DateOfBirth  WHERE Id = @ID";
                    await conn.OpenAsync();

                    await conn.QueryAsync<Author>(query,
                      new { ID = person.ID, Name = person.Name, Age = person.Age, NickName = person.Nickname, DateOfBirth = person.DateOfBirth });

                    return await GetByID(person.ID);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(UpdateAuthor)} : {e.Message}");
            }

            return null;
        }
    }
}

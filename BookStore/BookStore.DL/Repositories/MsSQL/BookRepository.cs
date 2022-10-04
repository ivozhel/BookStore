using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookStore.DL.Repositories.MsSQL
{
    public class BookRepository : IBookRepo
    {
        private readonly ILogger<BookRepository> _logger;
        private readonly IConfiguration _configuration;
        public BookRepository(ILogger<BookRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<Book> AddBook(Book book)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    await conn.QueryAsync<Book>("INSERT INTO Books VALUES (@AuthorId, @Title, @LastUpdated, @Quantity, @Price)",
                        new { AuthorId = book.AuthorId, Title = book.Title, LastUpdated = book.LastUpdated, Quantity = book.Quantity, Price = book.Price });
                    return book;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(AddBook)} : {e.Message}");
            }
            return null;
        }

        public async Task<Book> DeleteBook(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var deletedBook = await GetByID(id);
                    var query = $"DELETE FROM Books WHERE Id = {id}";
                    await conn.OpenAsync();

                    await conn.QueryAsync<Book>(query);
                    return deletedBook;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(DeleteBook)} : {e.Message}");
            }

            return null;
        }

        public async Task<IEnumerable<Book>> GetAllBook()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = "SELECT * FROM Books WITH(NOLOCK)";
                    await conn.OpenAsync();

                    return await conn.QueryAsync<Book>(query);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllBook)} : {e.Message}");
            }

            return Enumerable.Empty<Book>();
        }

        public async Task<Book> GetByID(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = "SELECT * FROM Books WITH(NOLOCK) WHERE Id = @Id";
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Book>(query, new { Id = id });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetByID)} : {e.Message}");
            }

            return null;
        }

        public async Task<bool> HaveBooks(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = $"SELECT * FROM Books WITH(NOLOCK) WHERE AuthorId = @AuthorId";
                    await conn.OpenAsync();

                    var result = await conn.QueryFirstOrDefaultAsync<Book>(query, new { AuthorId = id });
                    if (result is null)
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetByID)} : {e.Message}");
            }

            return true;
        }

        public async Task<bool> IsBookDuplicated(BookRequest book)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = $"SELECT * FROM Books WITH(NOLOCK) WHERE Title = @Title AND AuthorId = @AuthorId";
                    await conn.OpenAsync();

                    var result = await conn.QueryFirstOrDefaultAsync<Book>(query, new { Title = book.Title, AuthorId = book.AuthorId });
                    if (result is null)
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetByID)} : {e.Message}");
            }

            return true;
        }

        public async Task<Book> UpdateBook(Book book)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    await conn.QueryAsync<Book>("UPDATE Books SET AuthorId = @AuthorId, Title = @Title, LastUpdated = GETDATE(), Quantity = @Quantity, Price = @Price WHERE Id = @ID",
                        new { ID = book.ID, AuthorId = book.AuthorId, Title = book.Title, Quantity = book.Quantity, Price = book.Price });
                    return await GetByID(book.ID);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(AddBook)} : {e.Message}");
            }
            return null;
        }
    }
}

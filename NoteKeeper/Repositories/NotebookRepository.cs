
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NoteKeeper.Models;
using NoteKeeper.Repositories.Interfaces;

namespace NoteKeeper.Repositories
{
    public class NotebookRepository : INotebookRepository
    {
        private readonly string _connectionString;

        public NotebookRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<IEnumerable<Notebook>> GetAllAsync(string userId)
        {
            using var connection = CreateConnection();

            string sql = @"SELECT * FROM Notebooks
                           WHERE UserId = @UserId";

            return await connection.QueryAsync<Notebook>(
                sql, new { UserId = userId });
        }

        public async Task<Notebook?> GetByIdAsync(int id, string userId)
        {
            using var connection = CreateConnection();

            string sql = @"SELECT * FROM Notebooks
                           WHERE Id = @Id AND UserId = @UserId";

            return await connection.QueryFirstOrDefaultAsync<Notebook>(
                sql, new { Id = id, UserId = userId });
        }

        public async Task<int> CreateAsync(Notebook notebook)
        {
            using var connection = CreateConnection();

            string sql = @"INSERT INTO Notebooks (Title, Description, UserId)
                           VALUES (@Title, @Description, @UserId);
                           SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return await connection.ExecuteScalarAsync<int>(sql, notebook);
        }

        public async Task<bool> UpdateAsync(Notebook notebook)
        {
            using var connection = CreateConnection();

            string sql = @"UPDATE Notebooks
                           SET Title = @Title, Description = @Description
                           WHERE Id = @Id AND UserId = @UserId";

            int rows = await connection.ExecuteAsync(sql, notebook);

            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id, string userId)
        {
            using var connection = CreateConnection();

            string sql = @"DELETE FROM Notebooks
                           WHERE Id = @Id AND UserId = @UserId";

            int rows = await connection.ExecuteAsync(
                sql, new { Id = id, UserId = userId });

            return rows > 0;
        }
    }
}

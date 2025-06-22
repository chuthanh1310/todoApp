using AutoMapperMVC.Models;
using Microsoft.Data.SqlClient;

namespace AutoMapperMVC.Repository
{
    public class TodoRepository
    {
        private readonly string _connectionString;
        public TodoRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }
        public List<Todo> GetAll()
        {
            var list = new List<Todo>();
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var command = new SqlCommand("SELECT * FROM Todos", connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Todo
                {
                    Id = (int)reader["Id"],
                    Title = reader["Title"].ToString()!,
                    IsCompleted = (bool)reader["IsCompleted"]
                });
            }
            return list;
        }
        public void Add(Todo todo)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var command = new SqlCommand("INSERT INTO Todos (Title, IsCompleted) VALUES (@Title, @IsCompleted)", connection);
            command.Parameters.AddWithValue("@Title", todo.Title);
            command.Parameters.AddWithValue("@IsCompleted", todo.IsCompleted);
            command.ExecuteNonQuery();
        }
        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var command = new SqlCommand("DELETE FROM Todos WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }
        public void DeleteCompleted(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var cmd = new SqlCommand("DELETE FROM Todos WHERE IsCompleted = 1", connection);
            cmd.ExecuteNonQuery();
        }
        public void Toggle(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var cmd = new SqlCommand("UPDATE Todos SET IsCompleted = ~IsCompleted WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
        public void Update(Todo todo)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var command = new SqlCommand("UPDATE Todos SET Title = @Title, IsCompleted = @IsCompleted WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", todo.Id);
            command.Parameters.AddWithValue("@Title", todo.Title);
            command.Parameters.AddWithValue("@IsCompleted", todo.IsCompleted);
            command.ExecuteNonQuery();
        }
    }
}

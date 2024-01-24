using MiniPloomes.Dtos;
using MiniPloomes.Entities;
using System.Data.SqlClient;

namespace MiniPloomes.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public UserRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetSection("ConnectionString")["DefaultConnection"]!;
    }


    public User? Get(int id)
    {
        using var connection = new SqlConnection(_connectionString);

        connection.Open();

        using var command = new SqlCommand(@"
            SELECT TOP 1 * FROM [User] WHERE Id = @Id 
        ", connection);

        command.Parameters.AddWithValue("@Id", id);

        using var reader = command.ExecuteReader();

        if (!reader.Read())
            return null;

        var user = new User
        {
            Id = Convert.ToInt32(reader["Id"]),
            Name = reader["Name"].ToString()!,
            Email = reader["Email"].ToString()!
        };

        return user;
    }

    public IEnumerable<User> List()
    {
        List<User> users = new List<User>();

        using var connection = new SqlConnection(_connectionString);

        connection.Open();

        using var command = new SqlCommand(@"
            SELECT * FROM [User]
        ", connection);

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var user = new User
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString()!,
                Email = reader["Email"].ToString()!
            };

            users.Add(user);
        }

        return users;
    }

    public void Create(CreateUserDto userDto)
    {
        using var connection = new SqlConnection(_connectionString);

        connection.Open();

        using var command = new SqlCommand(@"
            INSERT INTO [User]
            (Name, Email)
            VALUES
            (@Name, @Email)
        ", connection);

        command.Parameters.AddWithValue("@Name", userDto.Name);
        command.Parameters.AddWithValue("@Email", userDto.Email);

        command.ExecuteNonQuery();

        return;
    }

}

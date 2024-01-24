using MiniPloomes.Dtos;
using MiniPloomes.Entities;
using System.Data.SqlClient;
using System.Windows.Input;

namespace MiniPloomes.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public CustomerRepository(IConfiguration configuration, IUserRepository userRepository)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetSection("ConnectionString")["DefaultConnection"]!;
        _userRepository = userRepository;
    }


    public Customer? Get(int id)
    {
        using var connection = new SqlConnection(_connectionString);

        connection.Open();

        using var command = new SqlCommand(@"
            SELECT TOP 1 * FROM Customer WHERE Id = @Id 
        ", connection);

        command.Parameters.AddWithValue("@Id", id);

        using var reader = command.ExecuteReader();

        if (!reader.Read())
            return null;

        var customer = new Customer
        {
            Id = Convert.ToInt32(reader["Id"]),
            Name = reader["Name"].ToString()!,
            CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()!),
            UserId = Convert.ToInt32(reader["UserId"])
        };

        return customer;
    }

    public IEnumerable<Customer> List()
    {
        List<Customer> customers = new List<Customer>();

        using var connection = new SqlConnection(_connectionString);

        connection.Open();

        using var command = new SqlCommand(@"
            SELECT * FROM Customer
        ", connection);

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var customer = new Customer
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString()!,
                CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()!),
                UserId = Convert.ToInt32(reader["UserId"])
            };

            customers.Add(customer);
        }

        return customers;
    }

    public void Create(CreateCustomerDto customerDto)
    {
        var user = _userRepository.Get(customerDto.UserId);

        if (user is null)
            throw new Exception("User not found");

        using var connection = new SqlConnection(_connectionString);

        connection.Open();
        
        using var command = new SqlCommand(@"
            INSERT INTO Customer
            (Name, CreatedAt, UserId)
            VALUES
            (@Name, @CreatedAt, @UserId)
        ", connection);

        command.Parameters.AddWithValue("@Name", customerDto.Name);
        command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
        command.Parameters.AddWithValue("@UserId", customerDto.UserId);

        command.ExecuteNonQuery();

        return;
    }
}

using MiniPloomes.Dtos;
using MiniPloomes.Entities;

namespace MiniPloomes.Repositories;

public interface ICustomerRepository
{
    public void Create(CreateCustomerDto customerDto);
    public Customer? Get(int id);
    public IEnumerable<Customer> List();
}

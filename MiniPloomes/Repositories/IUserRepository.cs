using MiniPloomes.Dtos;
using MiniPloomes.Entities;

namespace MiniPloomes.Repositories;

public interface IUserRepository
{
    public void Create(CreateUserDto userDto);
    public User? Get(int id);
    public IEnumerable<User> List();
}

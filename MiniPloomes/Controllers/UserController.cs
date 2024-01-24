using Microsoft.AspNetCore.Mvc;
using MiniPloomes.Dtos;
using MiniPloomes.Entities;
using MiniPloomes.Repositories;

namespace MiniPloomes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("{id}")]
    public ActionResult<User> Get(int id)
    {
        var user = _userRepository.Get(id);

        if (user is null) return NotFound();

        return Ok(user);
    }

    [HttpGet]
    public ActionResult<List<User>> List()
    {
        return Ok(_userRepository.List());
    }

    [HttpPost]
    public ActionResult Create(CreateUserDto userDto)
    {
        _userRepository.Create(userDto);

        return Ok();
    }
}

using Microsoft.AspNetCore.Mvc;
using MiniPloomes.Dtos;
using MiniPloomes.Entities;
using MiniPloomes.Repositories;

namespace MiniPloomes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : Controller
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerController(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    [HttpGet("{id}")]
    public ActionResult<Customer> Get(int id)
    {
        var customer = _customerRepository.Get(id);

        if (customer is null) return NotFound();

        return Ok(customer);
    }

    [HttpGet]
    public ActionResult<List<Customer>> List()
    {
        return Ok(_customerRepository.List());
    }

    [HttpPost]
    public ActionResult Create(CreateCustomerDto customerDto)
    {
        _customerRepository.Create(customerDto);

        return Ok();
    }
}


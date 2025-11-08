using CleanTemplate.Logic.Repository;
using CleanTemplate.Model.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CleanTemplate.API.View.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private IRepository<User> _repository;

    public UserController(ILogger<UserController> logger,
        IRepository<User> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<IEnumerable<UserDTO>> Get()
    {
        var users = await new GetUsers(_repository).Execute();
        return users.Select(u => new UserDTO { Name = u.Name });
    }

    [HttpGet("{id}", Name = "GetUserById")]
    public async Task<ActionResult<UserDTO>> Get(long id)
    {
        var user = await new GetUserById(_repository).Execute(id);

        if (user is null)
        {
            return NotFound(new
            {
                message = "Usuario no encontrado"
            });
        }

        var roles = new List<string>();
        foreach (var rol in user.Roles ?? Enumerable.Empty<Rol>())
        {
            roles.Add(rol.Name);
        }    
        
        return Ok(new UserDTO
        {
            Name = user.Name,
            Roles = roles
        });
    }
    
    // [HttpPost(Name = "PostUser")]
    // public async Task<IEnumerable<UserDTO>> Post()
    // {
    //     var users = await _repository.Get();
    //     return users.Select(u => new UserDTO { Name = u.Name});
    // }
}

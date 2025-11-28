using CleanTemplate.API.View.Helpers;
using CleanTemplate.Logic.Repository;
using CleanTemplate.Model.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanTemplate.API.View.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private IRepository<User> _repository;
    private readonly UtilsJWT _utilsJWT;

    public AuthController(IRepository<User> repository, UtilsJWT utilsJWT)
    {
        _repository = repository;
        _utilsJWT = utilsJWT;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("signup")]
    public async Task<IActionResult> SignUp(UserDTO user)
    {
        var modelo = new User
        {
            Name = user.Name,
            Password = _utilsJWT.EncriptarSHA256(user.Password)
        };

       var userCreated = await new SignUp(_repository).Execute(modelo);
        
        if (userCreated.Id != 0)
            return StatusCode(StatusCodes.Status201Created, new { isSuccess = true });
        else 
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("login")]
    public async Task<IActionResult> Login(LoginDTO login)
    {
        var usuarioEncontrado = await _repository.Get(u => u.Name == login.Name && u.Password == _utilsJWT.EncriptarSHA256(login.Password!));

        if(usuarioEncontrado == null) 
            return StatusCode(StatusCodes.Status404NotFound, new { isSuccess = false, token = "" });
        else
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _utilsJWT.GenerarJWT(usuarioEncontrado)});
    }

    [HttpGet]
    [Route("validarToken")]
    /// <summary>
    /// Recibe el token y controla su validez. 
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public IActionResult ValidarToken([FromQuery] string token)
    {
        bool tokenValido = _utilsJWT.ValidarToken(token);
        return StatusCode(StatusCodes.Status200OK, new { isSuccess = tokenValido });
    }

}
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
    public async Task<IActionResult> SignUp(LoginDTO request)
    {

        var userCreated = await new SignUp(_repository).Execute(
            new User
            {
                Name = request.Name,
                Password = _utilsJWT.EncryptSHA256(request.Password)
            }
        );
        
        if (userCreated.Id != 0)
            return StatusCode(StatusCodes.Status201Created, new { isSuccess = true });
        else 
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("login")]
    public async Task<IActionResult> Login(LoginDTO request)
    {
        var loggedInUser = await new LogIn(_repository)
            .Execute(request.Name, _utilsJWT.EncryptSHA256(request.Password!));

        if(loggedInUser == null)
        {            
            return StatusCode(
                StatusCodes.Status404NotFound, 
                new { isSuccess = false, token = "" }
            );
        }
        else
        {
            return StatusCode(
                StatusCodes.Status200OK, 
                new { isSuccess = true, token = _utilsJWT.GenerateJWT(loggedInUser)}
            );
        }
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

    [HttpPost]
    [Route("logout")]
    public async Task<ActionResult> Logout()
    {
        // Obtener el token del header Authorization
        var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        
        if (string.IsNullOrEmpty(token))
            return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = false, message = "Token no proporcionado" });
        
        // Agregar el token a la blacklist
        _utilsJWT.InvalidateToken(token);
        
        return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, message = "Sesión cerrada correctamente" });
    }

}
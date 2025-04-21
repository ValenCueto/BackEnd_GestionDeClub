using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ICustomAuthenticationService _customAuthenticationService;

        public AuthenticationController(IConfiguration config, ICustomAuthenticationService autenticacionService)
        {
            _config = config; //Hacemos la inyección para poder usar el appsettings.json
            _customAuthenticationService = autenticacionService;
        }

        /// <summary>
        /// Authenticates a user
        /// </summary>
        /// <remarks>
        /// Return a JWT token for the user logged in, with a role claim iqual to userType passed in the body.
        /// UserType value must be "Professor" or "Student", case sensitive.
        /// </remarks>
        [HttpPost("authenticate")] //Vamos a usar un POST ya que debemos enviar los datos para hacer el login
        public ActionResult<string> Authenticate(AuthenticationRequest authenticationRequest) //Enviamos como parámetro la clase que creamos arriba
        {
            try
            {
                string token = _customAuthenticationService.Authenticate(authenticationRequest); //Lo primero que hacemos es llamar a una función que valide los parámetros que enviamos.

                return Ok(token);
            }
            catch (UnauthorizedException ex)
            {
                return Unauthorized(ex.Message);
            }

        }
    }
}

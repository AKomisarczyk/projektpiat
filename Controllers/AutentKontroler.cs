using Microsoft.AspNetCore.Mvc;
using Transf;
using Usługi.Interfejsy;
using System.Linq;
using System.Threading.Tasks;
using WymianaWaluty.Modele;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;
using WymianaWaluty.Usługi.Implementacje;
using WymianaWaluty.Usługi.Interfejsy;

namespace WymianaWaluty.Kontrolery
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutentKontroler : ControllerBase
    {
        private readonly IAutSerwis _authService;

        public AutentKontroler(IAutSerwis authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRqs request)
        {
            var result = await _authService.RegisterAsync(request);
            if (!result)
            {
                return BadRequest(new { message = "Registration failed" });
            }

            return Ok(new { message = "Registration successful" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRcs request)
        {
            var token = await _authService.LoginAsync(request);

            if (token == null)
            {
                return Unauthorized(new { message = "Login failed" });
            }

            return Ok(new { token });
        }
    }
}
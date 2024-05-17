using System.Linq;
using System.Threading.Tasks;
using WymianaWaluty.Modele;
using WymianaWaluty.Usługi.Interfejsy;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Modele;
using Transf;
using Usługi.Interfejsy;

namespace WymianaWaluty.Usługi.Implementacje
{
    public class AutSerwis : IAutSerwis
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AutSerwis(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> RegisterAsync(RegisterRqs request)
        {
            _context.Users.Add(new User { Username = request.Username, Password = request.Password });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string> LoginAsync(LoginRcs request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Username && x.Password == request.Password);

            if (user == null)
            {
                Console.WriteLine("Login failed: User not found or password incorrect");
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Username) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
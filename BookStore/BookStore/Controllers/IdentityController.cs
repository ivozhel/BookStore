using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStore.BL.Interfaces;
using BookStore.Models.Models.Users;
using BookStore.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IIdentityService _identityService;
        public IdentityController(IConfiguration configuration, IIdentityService identityService)
        {
            _configuration = configuration;
            _identityService = identityService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginReq)
        {
            if (loginReq != null && !string.IsNullOrEmpty(loginReq.Username) && !string.IsNullOrEmpty(loginReq.Password))
            {
                var user = await _identityService.CheckUserAndPass(loginReq.Username, loginReq.Password);

                if (user != null)
                {
                    var userRoles = await _identityService.GetUserRoles(user);
                    var claims = new List<Claim>()
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,_configuration.GetSection("Jwt:Subject").Value),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToString()),
                        new Claim("UserId",user.UserId.ToString()),
                        new Claim("DisplayName",user.DisplayName ?? string.Empty),
                        new Claim("UserName",user.UserName ?? string.Empty), 
                        new Claim("Email",user.Email ?? string.Empty),
                        //new Claim("Admin","Admin")
                    };
                    foreach (var role in userRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role,role));
                    }

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims,
                                expires: DateTime.UtcNow.AddMinutes(10), signingCredentials: signIn);
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                return BadRequest("Invalid credentials");
            }
            return BadRequest("No passward or email not entered");
        }

        [AllowAnonymous]
        [HttpPost(nameof(CreateUser))]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("You need to enter username and passward");
            }

            var result = await _identityService.CreateAsync(user);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TodoAPIClass.Models;
using TodoAPIClass.ViewModel;

namespace TodoAPIClass.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthenticationController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _roleManager= roleManager;
            _userManager= userManager;
            _configuration= configuration;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists= await _userManager.FindByNameAsync(model.Username);
            if (userExists != null) 
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { IsSuccess = false, Message = "user exists" });

            var user = new User()
            {
                Email = model.Email,
                UserName=model.Username,
                SecurityStamp= Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { IsSuccess = false, Message = result.Errors.ToString() });


            return Ok(new ResponseModel { IsSuccess = true, Message = "successfully created user" });
        }


        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { IsSuccess = false, Message = "user exists" });

            var user = new User()
            {
                Email = model.Email,
                UserName = model.Username,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { IsSuccess = false, Message = result.Errors.ToString() });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin)) {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin)); }

            else {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok(new ResponseModel { IsSuccess = true, Message = "admin user created and assigned to role" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSignkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(5),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSignkey, SecurityAlgorithms.HmacSha256));

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

           return Unauthorized();
            
        }
    }
}

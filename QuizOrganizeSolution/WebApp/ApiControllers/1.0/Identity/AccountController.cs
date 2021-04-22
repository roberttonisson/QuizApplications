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
using BLL.App.DTO.CustomDTO;
using Extensions.testAuth;
using Models.Identity;


namespace WebApp.ApiControllers._1._0.Identity
{
        /// <summary>
    /// Api endpoint for registering new user and user log-in (jwt token generation)
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]

    public class AccountController : ControllerBase
    {
         private readonly UserManager<AppUser> userManager;  
        private readonly IConfiguration _configuration;  
  
        public AccountController(UserManager<AppUser> userManager, IConfiguration configuration)  
        {  
            this.userManager = userManager;  
            _configuration = configuration;  
        }  
  
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)  
        {  
            var user = await userManager.FindByNameAsync(model.Email);  
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))  
            {  
                var userRoles = await userManager.GetRolesAsync(user);  
  
                var authClaims = new List<Claim>  
                {  
                    new Claim(ClaimTypes.Name, user.UserName),  
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  
                };  
  
                foreach (var userRole in userRoles)  
                {  
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));  
                }  
  
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));  
  
                var token = new JwtSecurityToken(  
                    issuer: _configuration["JWT:ValidIssuer"],  
                    audience: _configuration["JWT:ValidAudience"],  
                    expires: DateTime.Now.AddHours(24),  
                    claims: authClaims,  
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)  
                    );

                return Ok(new JwtResponseDTO
                {
                    AppUserId = user.Id,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Status = "logged in",
                    FirstName = user.FirstName,
                    LastName = user.LastName
                });
            }  
            return Unauthorized();  
        }  
  
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)  
        {  
            var userExists = await userManager.FindByNameAsync(model.Email);  
            if (userExists != null)  
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });  
  
            AppUser user = new AppUser()  
            {  
                Email = model.Email,  
                SecurityStamp = Guid.NewGuid().ToString(),  
                FirstName  = model.FirstName,
                LastName = model.LastName, 
                UserName  = model.Email
                
            };  
            var result = await userManager.CreateAsync(user, model.Password);  
            if (!result.Succeeded)  
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });  
  
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });  
        }  
    }
    
    

    /// <summary>
    /// DTO for login validation
    /// </summary>
    public class LoginDTO
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } = default!;
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; } = default!;
    }

    /// <summary>
    /// DTO for register validation
    /// </summary>
    public class RegisterDTO
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } = default!;
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; } = default!;
        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; } = default!;
        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; } = default!;
    }
    
    /// <summary>
    /// DTO for changing password
    /// </summary>
    public class ChangePasswordDTO
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } = default!;
        /// <summary>
        /// Old password
        /// </summary>
        public string OldPassword { get; set; } = default!;
        /// <summary>
        /// Password that we want to change to
        /// </summary>
        public string NewPassword { get; set; } = default!;
    }
    
    /// <summary>
    /// DTO for changing email
    /// </summary>
    public class ChangeEmailDTO
    {
        /// <summary>
        /// Old email
        /// </summary>
        public string Email { get; set; } = default!;
        /// <summary>
        /// New email
        /// </summary>
        public string NewEmail { get; set; } = default!;
    }
    
}

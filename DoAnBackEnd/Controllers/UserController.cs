using DoAn.Domain.Entities;
using DoAn.Service.AppRoleService;
using DoAn.Service.AppUserRoleService;
using DoAn.Service.Constants;
using DoAn.Service.Dtos;
using DoAn.Service.Dtos.UserDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoAnBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IAppUserRoleService _appUserRoleService;
        private readonly IAppRoleService _appRoleService;

        public UserController(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IConfiguration configuration,
            IAppRoleService appRoleService,
            IAppUserRoleService appUserRoleService
            
           
        ) 
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _appUserRoleService = appUserRoleService;
            _appRoleService = appRoleService;


        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                //var userRoles = await _userManager.GetRolesAsync(user);validate-token

                var userRoles = await _appUserRoleService.GetAllRoleByUser(user);
                

                //tạo claim cho payload
                var authClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var userRole in userRoles)
                {
                    var role = await _roleManager.FindByIdAsync(userRole);
                    if (role != null)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                        var roleClaims = await _roleManager.GetClaimsAsync(role);
                        if (roleClaims != null)
                        {
                            foreach (var roleClaim in roleClaims)
                            {
                                authClaims.Add(roleClaim);
                            }
                        }
                    }
                }

                //lay JWT secret và mã hóa chữ ký
                var authSignignKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                //Convert time
                TimeZone time = TimeZone.CurrentTimeZone;
                DateTime dateNow = time.ToUniversalTime(DateTime.Now);
                var getSeTime = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                var seTime = TimeZoneInfo.ConvertTimeFromUtc(dateNow, getSeTime);

                //tạo token
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: seTime.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSignignKey, SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new
                {
                    userData = new
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        FullName = user.FullName,
                        PhoneNumber = user.PhoneNumber
                    },
                    accessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    expired = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            var userExists = await _userManager.FindByNameAsync(register.Username);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseWithMessageDto { Status = "Error", Message = "Tài khoản đã tồn tại." });
            }

            var emailExists = await _userManager.FindByEmailAsync(register.Email);
            if (emailExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseWithMessageDto { Status = "Error", Message = "Email đã tồn tại." });
            }

            AppUser user = new AppUser()
            {
                UserName = register.Username,
                Email = register.Email,
                FullName = register.FullName,
                PhoneNumber = register.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                return Ok(new ResponseWithMessageDto { Status = "Success", Message = "Đăng ký thành công" });
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseWithMessageDto { Status = "Error", Message = "Đăng ký thất bại" });
        }


        [HttpPost]
        [Route("registerForClient")]
        public async Task<IActionResult> RegisterForClient([FromBody] RegisterDto register)
        {
            var userExists = await _userManager.FindByNameAsync(register.Username);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseWithMessageDto { Status = "Error", Message = "Tài khoản đã tồn tại." });
            }

            var emailExists = await _userManager.FindByEmailAsync(register.Email);
            if (emailExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseWithMessageDto { Status = "Error", Message = "Email đã tồn tại." });
            }

            AppUser user = new AppUser()
            {
                UserName = register.Username,
                Email = register.Email,
                FullName = register.FullName,
                PhoneNumber = register.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, register.Password);
           

            if (result.Succeeded)
            {
                return Ok(new ResponseWithDataDto<AppUser>() { Status = "Success", Message = "Đăng ký thành công", Data = user });
               /* return Ok(result);*/
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseWithMessageDto { Status = "Error", Message = "Đăng ký thất bại" });
        }

        [HttpGet("validate-token")]
        [Authorize]
        public IActionResult ValidateToken()
        {
            // If the request reaches this point, it means the token is valid
            return Ok();
        }
    }
}

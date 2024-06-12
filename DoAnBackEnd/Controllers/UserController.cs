using DoAn.Domain.Entities;
using DoAn.Service.AppRoleService;
using DoAn.Service.AppUserRoleService;
using DoAn.Service.AppUserService;
using DoAn.Service.Constants;
using DoAn.Service.Dtos;
using DoAn.Service.Dtos.UserDto;
using DoAnBackEnd.Model.UserVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Dynamic.Core.Tokenizer;
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
        private readonly IAppUserService _appUserService;

        public UserController(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IConfiguration configuration,
            IAppRoleService appRoleService,
            IAppUserRoleService appUserRoleService,
            IAppUserService appUserService
           
        ) 
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _appUserRoleService = appUserRoleService;
            _appRoleService = appRoleService;
            _appUserService = appUserService;

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
                    new ResponseWithMessageDto { Status = "Error", Message = "Tài khoản đã tồn tại.", Code= 1 });
            }

            var emailExists = await _userManager.FindByEmailAsync(register.Email);
            if (emailExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseWithMessageDto { Status = "Error", Message = "Email đã tồn tại.", Code = 2 });
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

        [HttpGet("{id}")]
        public IActionResult GetUserById(Guid id)
        {

            try
            {
                var rs = _appUserService.GetByIdAndRole(id);
                return rs.Status == StatusConstant.SUCCESS ? StatusCode(StatusCodes.Status200OK, rs) : StatusCode(StatusCodes.Status500InternalServerError, rs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto
                {
                    Status = StatusConstant.ERROR,
                    Message = ex.Message
                });

            }
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] DoiMatKhauVM model)
        {
            // Kiểm tra model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Lấy user từ database
                var user = await _userManager.FindByIdAsync(model.UserId);

                // Kiểm tra user có tồn tại không
                if (user == null)
                {
                    return NotFound("User không tồn tại");
                }

                // Kiểm tra mật khẩu cũ có khớp với mật khẩu trong cơ sở dữ liệu không
                var passwordMatched = await _userManager.CheckPasswordAsync(user, model.OldPassword);

                if (!passwordMatched)
                {
                    // Nếu mật khẩu không khớp, xử lý tùy ý, ví dụ như trả về một phản hồi BadRequest
                    return BadRequest("Mật khẩu cũ không đúng.");
                }
                else
                {
                    // Update mật khẩu
                    var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (!result.Succeeded)
                    {
                        return BadRequest(result.Errors);
                    }
                    return Ok(new { status = "Success", message = "Mật khẩu đã được thay đổi thành công" });
                }


                
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(UpdateVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Cập nhật thông tin người dùng
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.FullName = model.FullName;

            // Gọi phương thức UpdateAsync để cập nhật thông tin
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok("Cập nhật thành công");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }
}

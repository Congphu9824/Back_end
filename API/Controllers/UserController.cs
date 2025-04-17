using Back_end.Services;
using DATA.DTO;
using DATA.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService _IUserService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {

            var user = await _IUserService.Register(request);

            if (user == null)
            {
                Log.Warning("Registration failed ", request.UserName);
            }
            Log.Information("User registered successfully:", user.UserName, user.Id);

            return Ok(new
            {
                Message = "Registration successful",
                User = new
                {
                    user.Id,
                    user.FullName,
                    user.Email,
                    user.UserName
                }
            });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _IUserService.Login(request.UserName, request.Password);
            if (user == null)
            {
                Log.Warning("Login failed: {UserName}", request.UserName);
                return Unauthorized(new { message = "Invalid username or password" });
            }

            // Generate JWT token
            var accessToken = _IUserService.GenerateJwtToken(user.UserName, user.Role?.Name, user.Id.ToString(), user.FullName);

            var refreshToken = await _IUserService.GenerateRefreshToken(user.Id, "DeviceInfoHere"); 


            // Attach tokens to cookies
            Response.Cookies.Append("AccessToken", accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(15)
            });

            //
            Response.Cookies.Append("RefreshToken", refreshToken.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7) 
            });

            Log.Information("Login successful: ", user.UserName);

            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                ExpiresIn = 15 * 60,
                User = new
                {
                    user.Id,
                    user.FullName,
                    user.Email,
                    Role = user.Role?.CodeRole
                }
            });
        }

        [HttpPost("add-role")]
        public async Task<IActionResult> AddRole([FromBody] AddRoleRequest request)
        {
            Log.Information("Attempting to add new role", request.Name, request.CodeRole);

            var success = await _IUserService.AddRole(request.Name, request.CodeRole, request.CreateBy);
            if (!success)
            {
                Log.Warning("Failed to add role. Role {RoleCode} already exists.", request.CodeRole);
                return BadRequest("Role already exists");
            }

            Log.Information("Role added successfully", request.Name, request.CodeRole);
            return Ok("Role added successfully" + success);
        }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var tokenEntity = await _IUserService.GetRefreshToken(refreshToken);
            if (tokenEntity == null || tokenEntity.IsRevoked || tokenEntity.Expiry < DateTime.UtcNow)
            {
                return Unauthorized("Invalid or expired refresh token.");
            }

            // Fetch the user based on the refresh token's userId
            var user = await _IUserService.GetUserByIdAsync(tokenEntity.UserId);
            if (user == null) return Unauthorized("User not found.");

            var newAccessToken = _IUserService.GenerateJwtToken(user.UserName, user.Role?.Name, user.Id.ToString(), user.FullName);

            return Ok(new
            {
                AccessToken = newAccessToken,
                ExpiresIn = 15 * 60
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // Optionally revoke refresh token
            var refreshToken = Request.Cookies["RefreshToken"];
            if (!string.IsNullOrEmpty(refreshToken))
            {
                await _IUserService.RevokeRefreshToken(refreshToken);
            }

            Response.Cookies.Delete("AccessToken");
            Response.Cookies.Delete("RefreshToken"); // Optional if you're using refresh token in cookies

            Log.Information("User logged out successfully.");
            return Ok(new { message = "Logged out successfully" });
        }


    }
}

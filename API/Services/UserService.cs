using Data.Context;
using DATA.DTO;
using DATA.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Back_end.Services
{
    public interface IUserService
    {
        Task<User?> GetUserByIdAsync(Guid userId);


        Task<User> Register(RegisterRequest request);
        Task<User> Login(string username, string password);
        string GenerateJwtToken(string username, string role, string userId, string fullName);
        Task<bool> AddRole(string roleName, string roleCode, string? createBy);

        Task<RefreshToken> GenerateRefreshToken(Guid userId, string deviceInfo);
        Task RevokeRefreshToken(string token);
        Task<RefreshToken?> GetRefreshToken(string token);

    }

    public class UserService(ModelDbContext _db, IConfiguration _configuration) : IUserService
    {
        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _db.User
                            .Include(u => u.Role)
                            .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> AddRole(string roleName, string roleCode, string? createBy)
        {
            if (await _db.Role.AnyAsync(r => r.CodeRole == roleCode))
            {
                return false;
            }

            var newRole = new Role
            {
                Id = Guid.NewGuid(),
                Name = roleName,
                CodeRole = roleCode,
                CreatedTime = DateTime.Now,
                CreateBy = createBy
            };

            await _db.Role.AddAsync(newRole);
            await _db.SaveChangesAsync();

            return true;
        }

        // Generate a JWT Token containing the logged in user's information.
        public string GenerateJwtToken(string username, string role, string userId, string fullName)
        {
            // infor user
            var claims = new[]
                       {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
                new Claim("UserId", userId),
                new Claim("FullName", fullName)
            };
            // Security key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            //Signature 
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Generate JWT Token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:AccessTokenExpiresMinutes"])),
                signingCredentials: creds);

            // return string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User> Login(string username, string password)
        {
            var hashedPassword = HashPassword(password);
            return await _db.User
                            .Include(u => u.Role)
                            .FirstOrDefaultAsync(u => u.UserName == username && u.Password == hashedPassword);
        }

        public async Task<User?> Register(RegisterRequest request)
        {
            var defaultRole = await _db.Role.FirstOrDefaultAsync(r => r.CodeRole == "User");
            if (defaultRole == null) return null;

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                UserName = request.UserName,
                Password = HashPassword(request.Password), 
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                RoleId = defaultRole.Id,    
                CreateBy = request.FullName
            };

            await _db.User.AddAsync(newUser);
            await _db.SaveChangesAsync();

            return newUser;
        }

        public async Task<RefreshToken> GenerateRefreshToken(Guid userId, string deviceInfo)
        {
            // Generate random numbers:
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            // convert random string to base64
            var rawToken = Convert.ToBase64String(randomNumber); // Đây là token client cần

            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = RefreshToken.HashToken(rawToken),
                Expiry = DateTime.UtcNow.AddDays(7),
                DeviceInfo = deviceInfo,
                UserId = userId
            };

            await _db.RefreshTokens.AddAsync(refreshToken);
            await _db.SaveChangesAsync();

            // Reassign raw tokens to pay for client/ no save db
            refreshToken.Token = rawToken;

            return refreshToken;
        }



        public async Task<RefreshToken?> GetRefreshToken(string token)
        {
            var hashedToken = RefreshToken.HashToken(token);
            return await _db.RefreshTokens.FirstOrDefaultAsync(r => r.Token == hashedToken && !r.IsRevoked && r.Expiry > DateTime.UtcNow);
        }

        //thu hỗi Refresh Token / logout 
        public async Task RevokeRefreshToken(string token)
        {
            // hash token
            var hashedToken = RefreshToken.HashToken(token);
            var refreshToken = await _db.RefreshTokens.FirstOrDefaultAsync(r => r.Token == hashedToken);
            if (refreshToken != null)
            {
                refreshToken.IsRevoked = true;
                await _db.SaveChangesAsync();
            }
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

    }
}

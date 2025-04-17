using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DATA.Entities
{
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }

        public string Token { get; set; } // Hashed token
        public DateTime Expiry { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public string DeviceInfo { get; set; } // User-Agent, IP...

        public bool IsRevoked { get; set; } = false;

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        // Hash the refresh token before saving it
        public static string HashToken(string token)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(token);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace DormitoryAPI.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        
        [Required]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        
        public string Role { get; set; } = "Tenant"; // Admin หรือ Tenant
        public string FullName { get; set; } = string.Empty;
        public int Points { get; set; } = 0;

        public string? PhoneNumber { get; set; }
        public string? LineID { get; set; }
    }
}
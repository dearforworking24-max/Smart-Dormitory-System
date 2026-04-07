using System.ComponentModel.DataAnnotations;

namespace DormitoryAPI.Models
{
    public class Room
    {
        [Key]
        public int RoomID { get; set; }
        
        [Required]
        public string RoomNumber { get; set; } = string.Empty;
        
        public string Status { get; set; } = "Available"; // Available, Occupied, Maintenance
        public decimal BaseRent { get; set; }
        
        // Foreign Key ไปหาตาราง User (ใช้ int? เพราะห้องอาจจะยังไม่มีคนเช่า)
        public int? TenantID { get; set; } 
        public User? Tenant { get; set; }
    }
}
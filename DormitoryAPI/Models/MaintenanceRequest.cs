using System;
using System.ComponentModel.DataAnnotations;

namespace DormitoryAPI.Models
{
    public class MaintenanceRequest
    {
        [Key]
        public int RequestID { get; set; }
        
        public int RoomID { get; set; }
        public int TenantID { get; set; }
        
        [Required]
        public string Description { get; set; } = string.Empty; 
        
        public string Status { get; set; } = "Pending"; 
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        
        public Room? Room { get; set; }
        public User? Tenant { get; set; }
    }
}
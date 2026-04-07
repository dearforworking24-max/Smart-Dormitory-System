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
        public string Description { get; set; } = string.Empty; // รายละเอียดปัญหา เช่น แอร์ไม่เย็น
        
        public string Status { get; set; } = "Pending"; // สถานะ: Pending (รอดำเนินการ), In Progress (กำลังซ่อม), Resolved (เสร็จสิ้น)
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // เชื่อมโยงกับตารางอื่น
        public Room? Room { get; set; }
        public User? Tenant { get; set; }
    }
}
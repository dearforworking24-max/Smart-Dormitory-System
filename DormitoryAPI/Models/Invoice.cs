using System.ComponentModel.DataAnnotations;

namespace DormitoryAPI.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceID { get; set; }
        
        // Foreign Key ไปหาตาราง Room
        public int RoomID { get; set; }
        public Room? Room { get; set; }
        
        public string BillingMonth { get; set; } = string.Empty; // เช่น "2026-04"
        public int WaterUnit { get; set; }
        public int ElectricUnit { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Unpaid"; // Unpaid หรือ Paid

        public string? PaymentSlipUrl { get; set; } // เก็บชื่อไฟล์ภาพสลิป
    }
}
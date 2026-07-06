namespace DormitoryAPI.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public int RoomID { get; set; }
        
        
        public int TenantID { get; set; } 
        
        public string? BillingMonth { get; set; }
        public int WaterUnit { get; set; }
        public int ElectricUnit { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; } 
        public string? PaymentSlipUrl { get; set; }
        
        public virtual Room? Room { get; set; }
    }
}
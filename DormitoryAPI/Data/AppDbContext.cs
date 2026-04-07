using Microsoft.EntityFrameworkCore;
using DormitoryAPI.Models; // ดึง Models ที่เราสร้างไว้มาใช้

namespace DormitoryAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // บอกให้ระบบรู้ว่าเราจะสร้างตารางอะไรบ้างใน Database
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }
    }
}
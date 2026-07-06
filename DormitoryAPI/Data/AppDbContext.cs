using Microsoft.EntityFrameworkCore;
using DormitoryAPI.Models; 

namespace DormitoryAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }
    }
}
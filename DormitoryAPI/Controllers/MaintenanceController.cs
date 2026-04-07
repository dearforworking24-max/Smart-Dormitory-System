using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DormitoryAPI.Data;
using DormitoryAPI.Models;

namespace DormitoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MaintenanceController(AppDbContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaintenanceRequest>>> GetAllRequests()
        {
            return await _context.MaintenanceRequests
                .Include(m => m.Room)
                .Include(m => m.Tenant)
                .OrderByDescending(m => m.CreatedDate)
                .ToListAsync();
        }

        
        [HttpGet("Tenant/{tenantId}")]
        public async Task<ActionResult<IEnumerable<MaintenanceRequest>>> GetTenantRequests(int tenantId)
        {
            return await _context.MaintenanceRequests
                .Include(m => m.Room)
                .Where(m => m.TenantID == tenantId)
                .OrderByDescending(m => m.CreatedDate)
                .ToListAsync();
        }

        
        [HttpPost]
        public async Task<ActionResult<MaintenanceRequest>> CreateRequest(MaintenanceRequest request)
        {
            request.CreatedDate = DateTime.Now;
            request.Status = "Pending"; 
            
            _context.MaintenanceRequests.Add(request);
            await _context.SaveChangesAsync();

            return Ok(new { message = "ส่งเรื่องแจ้งซ่อมเรียบร้อยแล้ว ช่างจะรีบเข้าไปดูแลครับ!" });
        }

        
        [HttpPut("{id}/Status/{status}")]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var request = await _context.MaintenanceRequests.FindAsync(id);
            if (request == null) return NotFound();

            request.Status = status;
            await _context.SaveChangesAsync();

            return Ok(new { message = "อัปเดตสถานะการซ่อมเรียบร้อยแล้ว" });
        }
    }
}
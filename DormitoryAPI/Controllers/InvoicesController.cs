using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DormitoryAPI.Data;
using DormitoryAPI.Models;

namespace DormitoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InvoicesController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            
            return await _context.Invoices
                                 .Include(i => i.Room) 
                                 .ToListAsync();
        }

        
        [HttpPost]
        public async Task<ActionResult<Invoice>> CreateInvoice(Invoice invoice)
        {
            
            invoice.TotalAmount = (invoice.WaterUnit * 20) + (invoice.ElectricUnit * 8);

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInvoices), new { id = invoice.InvoiceID }, invoice);
        }
        
        [HttpPost("Pay/{id}")]
        public async Task<IActionResult> PayInvoice(int id)
        {
            var invoice = await _context.Invoices.Include(i => i.Room).FirstOrDefaultAsync(i => i.InvoiceID == id);
            if (invoice == null) return NotFound();

            invoice.Status = "Paid";
            string returnMessage = "ชำระเงินสำเร็จ!";
            
            
            if (invoice.Room?.TenantID != null)
            {
                var user = await _context.Users.FindAsync(invoice.Room.TenantID);
                if (user != null)
                {
                    
                    int dueDate = 10; 

                    if (DateTime.Now.Day <= dueDate)
                    {
                        user.Points += 10;
                        returnMessage = "🎉 ชำระเงินสำเร็จ! คุณจ่ายก่อนกำหนด ได้รับ 10 แต้มสะสม";
                    }
                    else
                    {
                        returnMessage = "✅ ชำระเงินสำเร็จ! (เลยกำหนดชำระ จึงไม่ได้รับแต้มสะสมในรอบนี้)";
                    }
                }
            }

            await _context.SaveChangesAsync();

           
            return Ok(new { message = returnMessage });
        }
        
        [HttpGet("Tenant/{tenantId}")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetTenantInvoices(int tenantId)
        {
           
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.TenantID == tenantId);
            
            if (room == null) 
                return NotFound(new { message = "ไม่พบข้อมูลห้องพักของคุณ" });

           
            return await _context.Invoices
                                 .Include(i => i.Room)
                                 .Where(i => i.RoomID == room.RoomID)
                                 .ToListAsync();
        }
        [HttpPost("UploadSlip/{id}")]
        public async Task<IActionResult> UploadSlip(int id, IFormFile file)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null || file == null) return BadRequest();

            
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            
            var fileName = $"Slip_{id}_{DateTime.Now.Ticks}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            invoice.PaymentSlipUrl = "/uploads/" + fileName;
            await _context.SaveChangesAsync();

            return Ok(new { url = invoice.PaymentSlipUrl });
        }
    }
    
}
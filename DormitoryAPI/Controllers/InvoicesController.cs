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
            //  1. ดึงข้อมูลห้องเพื่อหาว่าใครคือผู้เช่าปัจจุบัน
            var room = await _context.Rooms.FindAsync(invoice.RoomID);
            
            if (room == null || room.TenantID == null)
            {
                return BadRequest(new { message = "ห้องนี้ยังไม่มีผู้เช่า ไม่สามารถออกบิลได้" });
            }

            //  2. ผูกบิลนี้เข้ากับไอดีของผู้เช่าโดยตรง
            invoice.TenantID = room.TenantID.Value;

            // คำนวณยอดรวม และเซ็ตสถานะเริ่มต้น
            invoice.TotalAmount = (invoice.WaterUnit * 20) + (invoice.ElectricUnit * 8);
            invoice.Status = "Unpaid";

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInvoices), new { id = invoice.InvoiceID }, invoice);
        }
        
        [HttpPost("Pay/{id}")]
        public async Task<IActionResult> PayInvoice(int id)
        {
            var invoice = await _context.Invoices.FirstOrDefaultAsync(i => i.InvoiceID == id);
            if (invoice == null) return NotFound();

            invoice.Status = "Paid";
            string returnMessage = "ชำระเงินสำเร็จ!";
            
            //  3. อ้างอิง TenantID จากตัวบิลได้เลย ไม่ต้องผ่านข้อมูล Room แล้ว
            var user = await _context.Users.FindAsync(invoice.TenantID);
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

            await _context.SaveChangesAsync();

            return Ok(new { message = returnMessage });
        }
        
        [HttpGet("Tenant/{tenantId}")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetTenantInvoices(int tenantId)
        {
            //  4. แก้ให้ดึงบิลจาก TenantID โดยตรง (อุดช่องโหว่สำเร็จ!)
            var invoices = await _context.Invoices
                                         .Include(i => i.Room)
                                         .Where(i => i.TenantID == tenantId) // ค้นหาด้วยรหัสผู้เช่าเท่านั้น
                                         .OrderByDescending(i => i.InvoiceID) // เรียงบิลใหม่ล่าสุดขึ้นก่อน
                                         .ToListAsync();

            return Ok(invoices);
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
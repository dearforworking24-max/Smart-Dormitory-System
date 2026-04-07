using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DormitoryAPI.Data;
using DormitoryAPI.Models;

namespace DormitoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsers), new { id = user.UserID }, user);
        }
        
        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login(LoginRequest request)
        {
            
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username && u.PasswordHash == request.Password);
            
            if (user == null) 
                return Unauthorized(new { message = "ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง" });

            return Ok(user);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "ไม่พบผู้ใช้งานนี้ในระบบ" });
            }

            
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.TenantID == id);
            if (room != null)
            {
                room.TenantID = null; 
                room.Status = "Available"; 
            }

            
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "ลบข้อมูลผู้ใช้งานเรียบร้อยแล้ว และอัปเดตสถานะห้องพัก (ถ้ามี)" });
        }
       
        [HttpPost("Redeem/{id}")]
        public async Task<IActionResult> RedeemPoints(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

           
            if (user.Points < 50)
            {
                return BadRequest(new { message = "แต้มของคุณยังไม่ถึง 50 แต้ม (สะสมต่ออีกนิดนะ!)" });
            }

            
            user.Points -= 50;
            await _context.SaveChangesAsync();

            return Ok(new { message = "🎁 แลกของรางวัลสำเร็จ! หัก 50 แต้มเรียบร้อย กรุณาติดต่อรับของที่นิติบุคคล" });
        }
       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User updatedUser)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound(new { message = "ไม่พบผู้ใช้งาน" });

            
            user.FullName = updatedUser.FullName;
            user.PhoneNumber = updatedUser.PhoneNumber;
            user.LineID = updatedUser.LineID;
            
            
            if (!string.IsNullOrEmpty(updatedUser.PasswordHash))
            {
                user.PasswordHash = updatedUser.PasswordHash;
            }

            await _context.SaveChangesAsync();
            return Ok(user); 
        }
        
        [HttpGet("AdminContact")]
        public async Task<IActionResult> GetAdminContact()
        {
            
            var admin = await _context.Users.FirstOrDefaultAsync(u => u.Role == "Admin");
            
            if (admin == null) 
            {
                return NotFound(new { message = "ยังไม่มีข้อมูลผู้ดูแลระบบ" });
            }

            return Ok(new { 
                phoneNumber = admin.PhoneNumber ?? "ยังไม่ระบุเบอร์โทร", 
                lineID = admin.LineID ?? "ยังไม่ระบุ LINE ID" 
            });
        }
    }
    
    
    
    
    // Login
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    
}
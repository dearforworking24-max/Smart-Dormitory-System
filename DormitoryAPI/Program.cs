using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.IO; 
using DormitoryAPI.Data; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers(); 

// 📌 1. ประกาศ CORS แค่รอบเดียวพอครับ
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// 📌 2. เรียกใช้ CORS ให้ไวที่สุด (ต้องมาก่อน Controllers และ Static Files)
app.UseCors("AllowAll"); 

// 📌 3. ปิด HttpsRedirection ชั่วคราวเวลาขึ้น Render ป้องกันการ Error
// app.UseHttpsRedirection(); 

var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
if (!Directory.Exists(uploadsPath))
{
    Directory.CreateDirectory(uploadsPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsPath),
    RequestPath = "/uploads"
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();   
    app.UseSwaggerUI(); 
}

app.MapControllers(); 

app.Run();
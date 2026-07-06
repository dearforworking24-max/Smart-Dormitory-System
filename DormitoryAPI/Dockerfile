# ใช้ .NET 8 SDK สำหรับสร้างโปรเจกต์
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# ก๊อปปี้ไฟล์ทั้งหมดและสั่ง Build
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# ใช้ .NET 8 Runtime สำหรับรันโปรแกรมจริง
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# ตั้งค่า Port ให้ Render รู้จัก
ENV ASPNETCORE_URLS=http://+:8080

# คำสั่งรันโปรแกรม (แก้ชื่อ DormitoryAPI.dll ให้ตรงกับชื่อโฟลเดอร์โปรเจกต์ของน้องเดียร์ถ้าจำเป็น)
ENTRYPOINT ["dotnet", "DormitoryAPI.dll"]
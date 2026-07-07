# 🏢 Smart Dormitory Management System

A full-stack web application designed to streamline dormitory management for both administrators and tenants. Built with a robust C# .NET backend and a responsive, modern frontend, deployed completely on the cloud.

## ✨ Key Features

**For Administrators (Admin Portal):**
* 📊 **Dashboard:** Real-time statistics and room occupancy ratio (Chart.js).
* 🛏️ **Room & Tenant Management:** Manage room status, track tenants, and auto-update availability.
* 🧾 **Billing & Invoicing:** Generate monthly bills and export professional Invoices as PDF.
* 🛠️ **Maintenance Tracking:** Receive, track, and update repair requests from tenants.

**For Tenants (Tenant Portal):**
* 💳 **Online Payment:** View pending invoices, upload payment slips, and pay bills online.
* 🛠️ **Maintenance Request:** Submit and track repair requests in real-time.
* 🌟 **Point System:** Earn points for activities and redeem rewards.
* 📱 **Profile Management:** Manage personal information and contact details.

## 💻 Tech Stack

**Backend & Database:**
* **Framework:** C# ASP.NET Core Web API (.NET 10)
* **Database:** PostgreSQL (Hosted on Neon)
* **ORM:** Entity Framework Core (Code-First)
* **Containerization:** Docker

**Frontend & UI:**
* **Core:** Vanilla HTML, JavaScript, CSS
* **Framework:** Bootstrap 5 (Bootswatch Flatly Theme)
* **Libraries:** jsPDF (PDF Generation), Chart.js (Data Visualization), Bootstrap Icons

**Cloud Deployment:**
* **Frontend:** Vercel
* **Backend API:** Render

## 🚀 How to Run the Project (Local Development)

1. Clone the repository:
   ```bash
   git clone [https://github.com/yourusername/Smart-Dormitory-System.git](https://github.com/yourusername/Smart-Dormitory-System.git)

## Website
1. Admin Please access this link https://smart-dormitory-system.vercel.app/
2. Tenant Please access this link https://smart-dormitory-system.vercel.app/tenant.html (maintenance)

## Setup
1.Configure the Database

Open appsettings.json in the Backend folder.

Update the DefaultConnection string with your PostgreSQL (Neon) connection string.

2. Apply Database Migrations
```bash 
  dotnet ef database update
```
3. Start the Backend Server
```bash
dotnet run
```
4. Run the Frontend
   Open index.html (Admin) or tenant.html (Tenant) in your browser using Live Server (VS Code Extension).

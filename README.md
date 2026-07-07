# 🏢 Smart Dormitory Management System

A full-stack web application designed to streamline dormitory management for both administrators and tenants. Built with a robust C# .NET backend and a responsive, modern frontend.

## ✨ Key Features
**For Administrators (Admin Portal):**
- 📊 **Dashboard:** Real-time statistics and room occupancy ratio (Chart.js).
- 🛏️ **Room & Tenant Management:** Manage room status, track tenants, and auto-update availability.
- 🧾 **Billing & Invoicing:** Generate monthly bills and export professional Invoices as PDF.
- 🛠️ **Maintenance Tracking:** Receive, track, and update repair requests from tenants.

**For Tenants (Tenant Portal):**
- 💳 **Online Payment:** View pending invoices, upload payment slips, and pay bills online.
- 🛠️ **Maintenance Request:** Submit and track repair requests in real-time.
- 🌟 **Point System:** Earn points for activities and redeem rewards.
- 📱 **Profile Management:** Manage personal information and contact details.

## 💻 Tech Stack
- **Backend:** C# ASP.NET Core Web API
- **Database:** SQLite with Entity Framework Core (Code-First)
- **Frontend:** Vanilla HTML, JavaScript, CSS
- **UI Framework:** Bootstrap 5 (Bootswatch Flatly Theme)
- **Tools & Libraries:** jsPDF (PDF Generation), Chart.js (Data Visualization), Bootstrap Icons

## 🚀 How to Run the Project
1. Clone the repository
2. Open terminal in the project directory
3. Run `dotnet ef database update` to set up the SQLite database
4. Run `dotnet run` to start the backend server
5. Open `index.html` (Admin) or `tenant.html` (Tenant) in your browser using Live Server.

## Website
1. Admin Please access this link https://smart-dormitory-system.vercel.app/
2. Tenant Please access this link https://smart-dormitory-system.vercel.app/tenant.html (maintenance)

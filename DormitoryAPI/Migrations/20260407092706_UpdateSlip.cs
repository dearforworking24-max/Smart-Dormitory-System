using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormitoryAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSlip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentSlipUrl",
                table: "Invoices",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentSlipUrl",
                table: "Invoices");
        }
    }
}

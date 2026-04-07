using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormitoryAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddLineID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LineID",
                table: "Users",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LineID",
                table: "Users");
        }
    }
}

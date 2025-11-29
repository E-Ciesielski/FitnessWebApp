using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddUsertoCaloriesLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CaloriesLogs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CaloriesLogs_UserId",
                table: "CaloriesLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CaloriesLogs_AspNetUsers_UserId",
                table: "CaloriesLogs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaloriesLogs_AspNetUsers_UserId",
                table: "CaloriesLogs");

            migrationBuilder.DropIndex(
                name: "IX_CaloriesLogs_UserId",
                table: "CaloriesLogs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CaloriesLogs");
        }
    }
}

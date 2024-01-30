using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChangePrice.Migrations
{
    public partial class Mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Alert_UserId",
                table: "Alert",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alert_AspNetUsers_UserId",
                table: "Alert",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alert_AspNetUsers_UserId",
                table: "Alert");

            migrationBuilder.DropIndex(
                name: "IX_Alert_UserId",
                table: "Alert");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChangePrice.Migrations
{
    public partial class InDB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "AlertAuto",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "AlertAuto");
        }
    }
}

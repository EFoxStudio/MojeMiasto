using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MojeMiasto.Migrations
{
    public partial class fix_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "region_id",
                table: "users",
                newName: "district_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "district_id",
                table: "users",
                newName: "region_id");
        }
    }
}

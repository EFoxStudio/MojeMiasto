using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MojeMiasto.Migrations
{
    public partial class quests_hired_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "hired_id",
                table: "quests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hired_id",
                table: "quests");
        }
    }
}

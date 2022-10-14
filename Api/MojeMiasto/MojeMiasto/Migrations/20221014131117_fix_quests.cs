using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MojeMiasto.Migrations
{
    public partial class fix_quests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "done",
                table: "quests",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "done",
                table: "quests");
        }
    }
}

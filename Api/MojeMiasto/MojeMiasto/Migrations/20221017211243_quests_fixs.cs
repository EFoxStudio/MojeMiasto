using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MojeMiasto.Migrations
{
    public partial class quests_fixs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reward",
                table: "quests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "reward",
                table: "quests",
                type: "longtext",
                nullable: false);
        }
    }
}

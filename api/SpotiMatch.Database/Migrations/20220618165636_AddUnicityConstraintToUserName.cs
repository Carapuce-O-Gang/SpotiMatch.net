using Microsoft.EntityFrameworkCore.Migrations;

namespace SpotiMatch.Database.Migrations
{
    public partial class AddUnicityConstraintToUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                table: "Users",
                column: "Name",
                unique: true,
                name: "UserNameIndex");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                table: "Users",
                name: "UserNameIndex");
        }
    }
}

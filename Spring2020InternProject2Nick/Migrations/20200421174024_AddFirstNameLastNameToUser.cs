using Microsoft.EntityFrameworkCore.Migrations;

namespace Spring2020InternProject2Nick.Migrations
{
    public partial class AddFirstNameLastNameToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "HRUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "HRUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "HRUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "HRUsers");
        }
    }
}

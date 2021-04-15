using Microsoft.EntityFrameworkCore.Migrations;

namespace NCBWebApp.Migrations
{
    public partial class Customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "Customer",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 7);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AccountNumber",
                table: "Customer",
                type: "int",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 7);
        }
    }
}

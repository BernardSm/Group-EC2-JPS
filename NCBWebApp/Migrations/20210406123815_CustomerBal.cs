using Microsoft.EntityFrameworkCore.Migrations;

namespace NCBWebApp.Migrations
{
    public partial class CustomerBal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "Customer",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Balance",
                table: "Customer",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}

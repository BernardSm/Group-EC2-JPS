using Microsoft.EntityFrameworkCore.Migrations;

namespace NCBWebApp.Migrations
{
    public partial class ChangeTransactionCusIdToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Customer_CustomerAccountId",
                table: "Transaction");

            migrationBuilder.RenameColumn(
                name: "CustomerAccountId",
                table: "Transaction",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_CustomerAccountId",
                table: "Transaction",
                newName: "IX_Transaction_CustomerId");

            migrationBuilder.AlterColumn<string>(
                name: "CusId",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Customer_CustomerId",
                table: "Transaction",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Customer_CustomerId",
                table: "Transaction");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Transaction",
                newName: "CustomerAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_CustomerId",
                table: "Transaction",
                newName: "IX_Transaction_CustomerAccountId");

            migrationBuilder.AlterColumn<int>(
                name: "CusId",
                table: "Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Customer_CustomerAccountId",
                table: "Transaction",
                column: "CustomerAccountId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

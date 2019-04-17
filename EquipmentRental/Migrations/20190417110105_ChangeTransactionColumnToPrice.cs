using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentRental.Migrations
{
    public partial class ChangeTransactionColumnToPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Invoice",
                table: "Transactions",
                newName: "Price");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Transactions",
                newName: "Invoice");
        }
    }
}

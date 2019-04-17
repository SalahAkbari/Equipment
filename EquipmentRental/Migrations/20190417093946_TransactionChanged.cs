using Microsoft.EntityFrameworkCore.Migrations;

namespace EquipmentRental.Migrations
{
    public partial class TransactionChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_EquipmentId",
                table: "Transactions",
                column: "EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Inventories_EquipmentId",
                table: "Transactions",
                column: "EquipmentId",
                principalTable: "Inventories",
                principalColumn: "InventoryID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Inventories_EquipmentId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_EquipmentId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "Transactions");
        }
    }
}

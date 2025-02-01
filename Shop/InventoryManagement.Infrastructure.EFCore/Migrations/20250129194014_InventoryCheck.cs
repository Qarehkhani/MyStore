using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryManagement.Infrastructure.EFCore.Migrations
{
    public partial class InventoryCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryOperation");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "InventoryOperations");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "InventoryOperations",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "InStock",
                table: "InventoryOperations",
                newName: "Operation");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "InventoryOperations",
                newName: "OperationDate");

            migrationBuilder.AddColumn<long>(
                name: "Count",
                table: "InventoryOperations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CurentCount",
                table: "InventoryOperations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "InventoryOperations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InventoryId",
                table: "InventoryOperations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OperatorId",
                table: "InventoryOperations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    UnitPrice = table.Column<double>(type: "float", nullable: false),
                    InStock = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryOperations_InventoryId",
                table: "InventoryOperations",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryOperations_Inventory_InventoryId",
                table: "InventoryOperations",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryOperations_Inventory_InventoryId",
                table: "InventoryOperations");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_InventoryOperations_InventoryId",
                table: "InventoryOperations");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "InventoryOperations");

            migrationBuilder.DropColumn(
                name: "CurentCount",
                table: "InventoryOperations");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "InventoryOperations");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "InventoryOperations");

            migrationBuilder.DropColumn(
                name: "OperatorId",
                table: "InventoryOperations");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "InventoryOperations",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "OperationDate",
                table: "InventoryOperations",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "Operation",
                table: "InventoryOperations",
                newName: "InStock");

            migrationBuilder.AddColumn<double>(
                name: "UnitPrice",
                table: "InventoryOperations",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "InventoryOperation",
                columns: table => new
                {
                    InventoryId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<long>(type: "bigint", nullable: false),
                    CurentCount = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Operation = table.Column<bool>(type: "bit", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OperatorId = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryOperation", x => new { x.InventoryId, x.Id });
                    table.ForeignKey(
                        name: "FK_InventoryOperation_InventoryOperations_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "InventoryOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}

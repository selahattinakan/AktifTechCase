using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AktifTech.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrder_Product_ProductId",
                table: "CustomerOrder");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOrder_ProductId",
                table: "CustomerOrder");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CustomerOrder");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "CustomerOrder");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "CustomerOrder");

            migrationBuilder.CreateTable(
                name: "OrderProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerOrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProduct_CustomerOrder_CustomerOrderId",
                        column: x => x.CustomerOrderId,
                        principalTable: "CustomerOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_CustomerOrderId",
                table: "OrderProduct",
                column: "CustomerOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductId",
                table: "OrderProduct",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "CustomerOrder",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "CustomerOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "CustomerOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrder_ProductId",
                table: "CustomerOrder",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrder_Product_ProductId",
                table: "CustomerOrder",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

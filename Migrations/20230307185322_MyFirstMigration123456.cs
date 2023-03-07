using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPTBOOK.Migrations
{
    public partial class MyFirstMigration123456 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Orders_Order_Id",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Cus_Phone",
                table: "Orders",
                newName: "cus_phone");

            migrationBuilder.RenameColumn(
                name: "Order_Id",
                table: "Orders",
                newName: "order_id");

            migrationBuilder.RenameColumn(
                name: "DeliveryLocal",
                table: "Orders",
                newName: "cus_last_name");

            migrationBuilder.RenameColumn(
                name: "Cus_Name",
                table: "Orders",
                newName: "cus_first_name");

            migrationBuilder.RenameColumn(
                name: "Order_Id",
                table: "Customers",
                newName: "order_id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Order_Id",
                table: "Customers",
                newName: "IX_Customers_order_id");

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Products",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "cus_address",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "cus_city",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "cus_email",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Orders_order_id",
                table: "Customers",
                column: "order_id",
                principalTable: "Orders",
                principalColumn: "order_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Orders_order_id",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "country",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "cus_address",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "cus_city",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "cus_email",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "cus_phone",
                table: "Orders",
                newName: "Cus_Phone");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "Orders",
                newName: "Order_Id");

            migrationBuilder.RenameColumn(
                name: "cus_last_name",
                table: "Orders",
                newName: "DeliveryLocal");

            migrationBuilder.RenameColumn(
                name: "cus_first_name",
                table: "Orders",
                newName: "Cus_Name");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "Customers",
                newName: "Order_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_order_id",
                table: "Customers",
                newName: "IX_Customers_Order_Id");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Orders_Order_Id",
                table: "Customers",
                column: "Order_Id",
                principalTable: "Orders",
                principalColumn: "Order_Id");
        }
    }
}

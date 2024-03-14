using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowerShopBusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SupplierAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Total = table.Column<int>(type: "int", nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    Freight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlowerBouquet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlowerBouquetName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    UnitPrice = table.Column<int>(type: "int", nullable: false),
                    UnitsInStock = table.Column<int>(type: "int", nullable: false),
                    FlowerBouquetStatus = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowerBouquet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowerBouquet_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlowerBouquet_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlowerBouquetID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitPrice = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => new { x.OrderID, x.FlowerBouquetID });
                    table.ForeignKey(
                        name: "FK_OrderDetail_FlowerBouquet_FlowerBouquetID",
                        column: x => x.FlowerBouquetID,
                        principalTable: "FlowerBouquet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountPassword", "CreateBy", "Created", "EmailAddress", "FullName", "IsDeleted", "LastModified", "LastModifiedBy", "Role" },
                values: new object[,]
                {
                    { new Guid("2477cb57-b562-469d-8f78-0a96663cb5e2"), "@5", null, new DateTime(2024, 3, 7, 18, 1, 11, 16, DateTimeKind.Local).AddTicks(8498), "customer@gmail.com", "Customer", false, null, null, 4 },
                    { new Guid("4694a7a2-e609-4bfc-bd6f-6f082367181d"), "@5", null, new DateTime(2024, 3, 7, 18, 1, 11, 16, DateTimeKind.Local).AddTicks(8496), "manager@gmail.com", "Manager", false, null, null, 3 },
                    { new Guid("c71f13e6-3a8f-4bfa-975e-c05dac1707eb"), "@5", null, new DateTime(2024, 3, 7, 18, 1, 11, 16, DateTimeKind.Local).AddTicks(8474), "staff@gmail.com", "Staff", false, null, null, 2 },
                    { new Guid("d38a7009-fa0c-4fd1-815f-49879dfcbfb7"), "@5", null, new DateTime(2024, 3, 7, 18, 1, 11, 16, DateTimeKind.Local).AddTicks(8443), "admin@gmail.com", "Administrator", false, null, null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreateBy", "Created", "Description", "IsDeleted", "LastModified", "LastModifiedBy" },
                values: new object[,]
                {
                    { new Guid("2c81c8f6-cae1-46a6-9bc4-71f29f6da74e"), "Rose", null, new DateTime(2024, 3, 7, 18, 1, 11, 16, DateTimeKind.Local).AddTicks(8636), "This is Rose", false, null, null },
                    { new Guid("b7101212-c852-4be6-8eed-031b096b2dd4"), "Peonies", null, new DateTime(2024, 3, 7, 18, 1, 11, 16, DateTimeKind.Local).AddTicks(8639), "This is Peonies", false, null, null },
                    { new Guid("cb85b07b-2fcc-497c-9007-8912a86c2f4f"), "Lily", null, new DateTime(2024, 3, 7, 18, 1, 11, 16, DateTimeKind.Local).AddTicks(8641), "This is Lily", false, null, null },
                    { new Guid("d302148f-1677-4094-8fa7-1c6b54ff8b69"), "Carnation", null, new DateTime(2024, 3, 7, 18, 1, 11, 16, DateTimeKind.Local).AddTicks(8649), "This is Carnation", false, null, null }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "CreateBy", "Created", "IsDeleted", "LastModified", "LastModifiedBy", "SupplierAddress", "SupplierName", "Telephone" },
                values: new object[,]
                {
                    { new Guid("f60db79f-1c7e-4b2b-a3f2-4551942ccdd6"), null, new DateTime(2024, 3, 7, 18, 1, 11, 16, DateTimeKind.Local).AddTicks(8674), false, null, null, "Dai Hoc FPT Co So 2, Ha Noi", "Ant Flower", "0123456789" },
                    { new Guid("fc1d6720-4461-418c-8680-8ad859eda033"), null, new DateTime(2024, 3, 7, 18, 1, 11, 16, DateTimeKind.Local).AddTicks(8670), false, null, null, "Dai Hoc FPT, Ha Noi", "Flower Shop Ciaoflora", "0123456789" }
                });

            migrationBuilder.InsertData(
                table: "FlowerBouquet",
                columns: new[] { "Id", "CategoryID", "CreateBy", "Created", "Description", "FlowerBouquetName", "FlowerBouquetStatus", "IsDeleted", "LastModified", "LastModifiedBy", "SupplierID", "UnitPrice", "UnitsInStock" },
                values: new object[,]
                {
                    { new Guid("4cee7a0f-17f9-4f49-aa3c-8dce72b8abe1"), new Guid("2c81c8f6-cae1-46a6-9bc4-71f29f6da74e"), null, new DateTime(2024, 3, 7, 18, 1, 11, 16, DateTimeKind.Local).AddTicks(8703), "This is Sun Flower", "Sun Flower", 1, false, null, null, new Guid("fc1d6720-4461-418c-8680-8ad859eda033"), 100000, 100 },
                    { new Guid("6cb4dd24-4dec-4ec2-8dab-5d677f11cffb"), new Guid("2c81c8f6-cae1-46a6-9bc4-71f29f6da74e"), null, new DateTime(2024, 3, 7, 18, 1, 11, 16, DateTimeKind.Local).AddTicks(8692), "This is Rose", "Red Rose", 1, false, null, null, new Guid("fc1d6720-4461-418c-8680-8ad859eda033"), 100000, 100 },
                    { new Guid("ccf6f1d5-beda-4832-8eae-1d82280693c0"), new Guid("2c81c8f6-cae1-46a6-9bc4-71f29f6da74e"), null, new DateTime(2024, 3, 7, 18, 1, 11, 16, DateTimeKind.Local).AddTicks(8700), "This is Orchis", "Orchis", 1, false, null, null, new Guid("fc1d6720-4461-418c-8680-8ad859eda033"), 100000, 100 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlowerBouquet_CategoryID",
                table: "FlowerBouquet",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_FlowerBouquet_SupplierID",
                table: "FlowerBouquet",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_AccountID",
                table: "Order",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_FlowerBouquetID",
                table: "OrderDetail",
                column: "FlowerBouquetID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "FlowerBouquet");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}

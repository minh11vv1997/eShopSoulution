using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class updatedatabase_productimg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("d276b5eb-7e1d-4347-96bf-ce5931bde593"),
                column: "ConcurrencyStamp",
                value: "ed917bcb-8f80-46ef-a267-0c50e289eccd");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("4ff07ec1-1521-4f74-92e8-b03f26a72a96"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cdb3ef98-b3dd-497e-a9b9-a1c723b2aee2", "AQAAAAEAACcQAAAAENJvuRF2LcHrQf/amZql5tmh8U4LpEfCbHWtTIz1p6KX0xX4gYSkBqG75+FSNDr2Yw==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 2, 2, 16, 32, 47, 186, DateTimeKind.Local).AddTicks(1683));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("d276b5eb-7e1d-4347-96bf-ce5931bde593"),
                column: "ConcurrencyStamp",
                value: "89805477-c8d8-4286-9703-7877dccbe196");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("4ff07ec1-1521-4f74-92e8-b03f26a72a96"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a85bf1fe-c51d-4dc1-85bd-0f449d45850d", "AQAAAAEAACcQAAAAEBDMeSqDCpq5qpQqVn4OD8DA7NZPUt+PQjO9QcnTUZcM68HLECX4lQVm47P6bglPeQ==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 2, 1, 9, 50, 52, 986, DateTimeKind.Local).AddTicks(4220));
        }
    }
}

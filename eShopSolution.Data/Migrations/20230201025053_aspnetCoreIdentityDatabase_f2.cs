using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class aspnetCoreIdentityDatabase_f2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("d276b5eb-7e1d-4347-96bf-ce5931bde593"), "89805477-c8d8-4286-9703-7877dccbe196", "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("4ff07ec1-1521-4f74-92e8-b03f26a72a96"), new Guid("d276b5eb-7e1d-4347-96bf-ce5931bde593") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("4ff07ec1-1521-4f74-92e8-b03f26a72a96"), 0, "a85bf1fe-c51d-4dc1-85bd-0f449d45850d", new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhvv@rikkeisoft.com", true, "Minh", "Vu", false, null, "minhvv@rikkeisoft.com", "admin", "AQAAAAEAACcQAAAAEBDMeSqDCpq5qpQqVn4OD8DA7NZPUt+PQjO9QcnTUZcM68HLECX4lQVm47P6bglPeQ==", null, false, "", false, "admin" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("d276b5eb-7e1d-4347-96bf-ce5931bde593"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("4ff07ec1-1521-4f74-92e8-b03f26a72a96"), new Guid("d276b5eb-7e1d-4347-96bf-ce5931bde593") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("4ff07ec1-1521-4f74-92e8-b03f26a72a96"));

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
                value: new DateTime(2023, 2, 1, 9, 10, 46, 603, DateTimeKind.Local).AddTicks(6174));
        }
    }
}

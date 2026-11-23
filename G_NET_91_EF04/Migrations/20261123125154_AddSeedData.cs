using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace G_NET_91_EF04.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "CustomerType", "DateOfBirth", "Email", "FullName", "NationalId", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "15 Nasr City, Cairo", 1, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ahmed.ali@example.com", "Ahmed Ali", "29001011234567", "01011112222" },
                    { 2, "8 Port Said St, Alexandria", 2, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@niletrading.com", "Nile Trading LLC", "30005551234567", "01233334444" }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "Email", "FullName", "HireDate", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "khaled.mostafa@nationalbank.com", "Khaled Mostafa", new DateTime(2015, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "01001234567" },
                    { 2, "laila.samir@nationalbank.com", "Laila Samir", new DateTime(2018, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "01007654321" }
                });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Code", "Address", "ManagerId", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { "ALX-01", "25 Corniche Road, Alexandria", 2, "Alexandria Branch", "0335551002" },
                    { "CAI-01", "10 Tahrir Square, Cairo", 1, "Cairo Main Branch", "0225551001" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountNumber", "AccountType", "BranchCode", "CurrentBalance", "OpeningDate" },
                values: new object[,]
                {
                    { "2001-CUR", 2, "CAI-01", 15400.00m, new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "3000-BUS", 3, "ALX-01", 120000.00m, new DateTime(2021, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "CustomerAccounts",
                columns: new[] { "AccountNumber", "CustomerId", "AccountStatus", "OwnershipStartDate", "OwnershipType" },
                values: new object[,]
                {
                    { "2001-CUR", 1, 1, new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { "3000-BUS", 2, 1, new DateTime(2021, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CustomerAccounts",
                keyColumns: new[] { "AccountNumber", "CustomerId" },
                keyValues: new object[] { "2001-CUR", 1 });

            migrationBuilder.DeleteData(
                table: "CustomerAccounts",
                keyColumns: new[] { "AccountNumber", "CustomerId" },
                keyValues: new object[] { "3000-BUS", 2 });

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountNumber",
                keyValue: "2001-CUR");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountNumber",
                keyValue: "3000-BUS");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Code",
                keyValue: "ALX-01");

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Code",
                keyValue: "CAI-01");

            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}

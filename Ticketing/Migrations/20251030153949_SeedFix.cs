using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketing.Migrations
{
    /// <inheritdoc />
    public partial class SeedFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e02fd0e4-00fd-090a-ca30-0d00a0038ba0"),
                columns: new[] { "Email", "Name" },
                values: new object[] { "cyclops@asdf.com", "Scott Summers" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e02fd0e4-00fd-090a-ca30-0d00a0038ba0"),
                columns: new[] { "Email", "Name" },
                values: new object[] { "scarletwitch@asdf.com", "Wanda Maximoff" });
        }
    }
}

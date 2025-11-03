using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ticketing.Migrations
{
    /// <inheritdoc />
    public partial class SeedTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "AssignedUserId", "CreatedAt", "Description", "TicketStatus", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("b6a5f2c8-9e44-4c1b-8f0d-2f8f65a7a3c1"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), new DateTime(2025, 11, 3, 10, 0, 0, 0, DateTimeKind.Unspecified), "Users cannot log in after latest update", 0, "Fix login issue", new DateTime(2025, 11, 3, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c9a1b2d3-4e5f-6789-abcd-0f1e2d3c4b5a"), new Guid("6b29fc40-ca47-1067-b31d-00dd010662da"), new DateTime(2025, 11, 3, 13, 0, 0, 0, DateTimeKind.Unspecified), "Schedule downtime for server upgrades", 2, "Server maintenance", new DateTime(2025, 11, 3, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d3e2c5f1-7c4b-4b8d-83e0-bb7d5cbb4f11"), new Guid("9a1b2c3d-4e5f-6789-abcd-1234567890ef"), new DateTime(2025, 11, 3, 11, 0, 0, 0, DateTimeKind.Unspecified), "Redesign profile page for better UX", 1, "Update user profile page", new DateTime(2025, 11, 3, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e1f2d3c4-5b6a-7c8d-9e0f-1a2b3c4d5e6f"), null, new DateTime(2025, 11, 3, 14, 0, 0, 0, DateTimeKind.Unspecified), "Some users are not receiving emails", 0, "Email notification bug", new DateTime(2025, 11, 3, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f8c4d7a2-1b2c-4d3f-a5e6-9f0b4c1d2e3f"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), new DateTime(2025, 11, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), "Payment failures reported on checkout", 2, "Fix payment gateway", new DateTime(2025, 11, 3, 12, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("b6a5f2c8-9e44-4c1b-8f0d-2f8f65a7a3c1"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("c9a1b2d3-4e5f-6789-abcd-0f1e2d3c4b5a"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("d3e2c5f1-7c4b-4b8d-83e0-bb7d5cbb4f11"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("e1f2d3c4-5b6a-7c8d-9e0f-1a2b3c4d5e6f"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("f8c4d7a2-1b2c-4d3f-a5e6-9f0b4c1d2e3f"));
        }
    }
}

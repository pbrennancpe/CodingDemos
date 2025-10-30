using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketing.Migrations
{
    /// <inheritdoc />
    public partial class TicketFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Tickets",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Tickets",
                newName: "Email");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketing.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketNo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "shared");

            migrationBuilder.CreateSequence<int>(
                name: "TicketNumbers",
                schema: "shared");

            migrationBuilder.AddColumn<int>(
                name: "TicketNo",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR shared.TicketNumbers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketNo",
                table: "Tickets");

            migrationBuilder.DropSequence(
                name: "TicketNumbers",
                schema: "shared");
        }
    }
}

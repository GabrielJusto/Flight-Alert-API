using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flight_Alert_API.Migrations
{
    /// <inheritdoc />
    public partial class Add_Link_On_Flight_Notification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "FlightNotifications",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "FlightNotifications");
        }
    }
}

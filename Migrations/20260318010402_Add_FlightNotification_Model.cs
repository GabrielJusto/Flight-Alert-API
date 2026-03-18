using Microsoft.EntityFrameworkCore.Migrations;

using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Flight_Alert_API.Migrations
{
    /// <inheritdoc />
    public partial class Add_FlightNotification_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlightNotifications",
                columns: table => new
                {
                    FlightNotificationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MonitoredRouteId = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightNotifications", x => x.FlightNotificationId);
                    table.ForeignKey(
                        name: "FK_FlightNotifications_MonitoredRoutes_MonitoredRouteId",
                        column: x => x.MonitoredRouteId,
                        principalTable: "MonitoredRoutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlightNotifications_MonitoredRouteId",
                table: "FlightNotifications",
                column: "MonitoredRouteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightNotifications");
        }
    }
}

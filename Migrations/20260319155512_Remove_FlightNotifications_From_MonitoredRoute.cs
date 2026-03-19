using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flight_Alert_API.Migrations
{
    /// <inheritdoc />
    public partial class Remove_FlightNotifications_From_MonitoredRoute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightNotifications_MonitoredRoutes_MonitoredRouteId",
                table: "FlightNotifications");

            migrationBuilder.DropIndex(
                name: "IX_FlightNotifications_MonitoredRouteId",
                table: "FlightNotifications");

            migrationBuilder.DropIndex(
                name: "IX_FlightNotifications_UserMonitoredRouteId",
                table: "FlightNotifications");

            migrationBuilder.DropColumn(
                name: "MonitoredRouteId",
                table: "FlightNotifications");

            migrationBuilder.CreateIndex(
                name: "IX_FlightNotifications_UserMonitoredRouteId",
                table: "FlightNotifications",
                column: "UserMonitoredRouteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FlightNotifications_UserMonitoredRouteId",
                table: "FlightNotifications");

            migrationBuilder.AddColumn<int>(
                name: "MonitoredRouteId",
                table: "FlightNotifications",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlightNotifications_MonitoredRouteId",
                table: "FlightNotifications",
                column: "MonitoredRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightNotifications_UserMonitoredRouteId",
                table: "FlightNotifications",
                column: "UserMonitoredRouteId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightNotifications_MonitoredRoutes_MonitoredRouteId",
                table: "FlightNotifications",
                column: "MonitoredRouteId",
                principalTable: "MonitoredRoutes",
                principalColumn: "Id");
        }
    }
}

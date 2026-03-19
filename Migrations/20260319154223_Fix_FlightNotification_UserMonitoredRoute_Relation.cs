using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flight_Alert_API.Migrations
{
    /// <inheritdoc />
    public partial class Fix_FlightNotification_UserMonitoredRoute_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightNotifications_MonitoredRoutes_MonitoredRouteId",
                table: "FlightNotifications");

            migrationBuilder.AlterColumn<int>(
                name: "MonitoredRouteId",
                table: "FlightNotifications",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "UserMonitoredRouteId",
                table: "FlightNotifications",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddForeignKey(
                name: "FK_FlightNotifications_UserMonitoredRoutes_UserMonitoredRouteId",
                table: "FlightNotifications",
                column: "UserMonitoredRouteId",
                principalTable: "UserMonitoredRoutes",
                principalColumn: "UserMonitoredRouteId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightNotifications_MonitoredRoutes_MonitoredRouteId",
                table: "FlightNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightNotifications_UserMonitoredRoutes_UserMonitoredRouteId",
                table: "FlightNotifications");

            migrationBuilder.DropIndex(
                name: "IX_FlightNotifications_UserMonitoredRouteId",
                table: "FlightNotifications");

            migrationBuilder.DropColumn(
                name: "UserMonitoredRouteId",
                table: "FlightNotifications");

            migrationBuilder.AlterColumn<int>(
                name: "MonitoredRouteId",
                table: "FlightNotifications",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightNotifications_MonitoredRoutes_MonitoredRouteId",
                table: "FlightNotifications",
                column: "MonitoredRouteId",
                principalTable: "MonitoredRoutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

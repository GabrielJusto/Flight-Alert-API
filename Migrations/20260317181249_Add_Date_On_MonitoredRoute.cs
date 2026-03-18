using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flight_Alert_API.Migrations
{
    /// <inheritdoc />
    public partial class Add_Date_On_MonitoredRoute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DepartureDay",
                table: "MonitoredRoutes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReturnDay",
                table: "MonitoredRoutes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartureDay",
                table: "MonitoredRoutes");

            migrationBuilder.DropColumn(
                name: "ReturnDay",
                table: "MonitoredRoutes");
        }
    }
}

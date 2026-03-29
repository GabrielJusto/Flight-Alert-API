using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flight_Alert_API.Migrations
{
    /// <inheritdoc />
    public partial class Add_IsActive_On_UserMonitoredRoute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "UserMonitoredRoutes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserMonitoredRoutes");
        }
    }
}

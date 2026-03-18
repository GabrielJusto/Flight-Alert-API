using Microsoft.EntityFrameworkCore.Migrations;

using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Flight_Alert_API.Migrations
{
    /// <inheritdoc />
    public partial class Add_Airport_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ident = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LatitudeDeg = table.Column<double>(type: "double precision", nullable: false),
                    LongitudeDeg = table.Column<double>(type: "double precision", nullable: false),
                    ElevationFt = table.Column<int>(type: "integer", nullable: true),
                    Continent = table.Column<string>(type: "text", nullable: false),
                    IsoCountry = table.Column<string>(type: "text", nullable: false),
                    IsoRegion = table.Column<string>(type: "text", nullable: false),
                    Municipality = table.Column<string>(type: "text", nullable: false),
                    ScheduledService = table.Column<string>(type: "text", nullable: false),
                    IcaoCode = table.Column<string>(type: "text", nullable: false),
                    IataCode = table.Column<string>(type: "text", nullable: false),
                    GpsCode = table.Column<string>(type: "text", nullable: false),
                    LocalCode = table.Column<string>(type: "text", nullable: false),
                    HomeLink = table.Column<string>(type: "text", nullable: false),
                    WikipediaLink = table.Column<string>(type: "text", nullable: false),
                    Keywords = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Airports");
        }
    }
}

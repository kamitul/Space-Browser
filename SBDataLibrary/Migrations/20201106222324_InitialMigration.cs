using Microsoft.EntityFrameworkCore.Migrations;

namespace SBDataLibrary.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Launches",
                columns: table => new
                {
                    FlightId = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Payloads = table.Column<int>(nullable: false),
                    RocketName = table.Column<string>(maxLength: 200, nullable: false),
                    Country = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    LaunchDate = table.Column<string>(maxLength: 200, nullable: true),
                    MissionName = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Launches", x => x.FlightId);
                });

            migrationBuilder.CreateTable(
                name: "Rockets",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RocketId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Type = table.Column<string>(maxLength: 100, nullable: false),
                    Country = table.Column<string>(maxLength: 100, nullable: false),
                    Mass = table.Column<int>(nullable: false),
                    Image = table.Column<byte[]>(nullable: true),
                    LaunchID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rockets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Rockets_Launches_LaunchID",
                        column: x => x.LaunchID,
                        principalTable: "Launches",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Type = table.Column<string>(maxLength: 100, nullable: false),
                    Missions = table.Column<int>(nullable: false),
                    HomePort = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    LaunchFlightId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ships_Launches_LaunchFlightId",
                        column: x => x.LaunchFlightId,
                        principalTable: "Launches",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rockets_LaunchID",
                table: "Rockets",
                column: "LaunchID",
                unique: true,
                filter: "[LaunchID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_LaunchFlightId",
                table: "Ships",
                column: "LaunchFlightId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rockets");

            migrationBuilder.DropTable(
                name: "Ships");

            migrationBuilder.DropTable(
                name: "Launches");
        }
    }
}

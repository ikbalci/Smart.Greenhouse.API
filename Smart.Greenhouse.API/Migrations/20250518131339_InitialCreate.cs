using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smart.Greenhouse.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SensorData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moisture = table.Column<double>(type: "float", nullable: false),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    AirQuality = table.Column<double>(type: "float", nullable: false),
                    PumperOn = table.Column<bool>(type: "bit", nullable: false),
                    HeaterOn = table.Column<bool>(type: "bit", nullable: false),
                    CoolerOn = table.Column<bool>(type: "bit", nullable: false),
                    TemperatureCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoistureCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeatingDemand = table.Column<double>(type: "float", nullable: false),
                    CoolingDemand = table.Column<double>(type: "float", nullable: false),
                    MoistureTemperatureRatio = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorData", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SensorData_CreatedAt",
                table: "SensorData",
                column: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SensorData");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smart.Greenhouse.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAirPurifierStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AirPurifierOn",
                table: "SensorData",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirPurifierOn",
                table: "SensorData");
        }
    }
}

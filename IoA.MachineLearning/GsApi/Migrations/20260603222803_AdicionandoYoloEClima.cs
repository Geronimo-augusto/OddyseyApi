using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GsApi.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoYoloEClima : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PressureHpa",
                table: "PredictionHistories",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TemperatureC",
                table: "PredictionHistories",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "YoloBirdCount",
                table: "PredictionHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "YoloMovementIndex",
                table: "PredictionHistories",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PressureHpa",
                table: "PredictionHistories");

            migrationBuilder.DropColumn(
                name: "TemperatureC",
                table: "PredictionHistories");

            migrationBuilder.DropColumn(
                name: "YoloBirdCount",
                table: "PredictionHistories");

            migrationBuilder.DropColumn(
                name: "YoloMovementIndex",
                table: "PredictionHistories");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SP23.P01.Web.Migrations
{
    /// <inheritdoc />
    public partial class renamedTranStation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainStation",
                table: "TrainStation");

            migrationBuilder.RenameTable(
                name: "TrainStation",
                newName: "TrainStations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainStations",
                table: "TrainStations",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainStations",
                table: "TrainStations");

            migrationBuilder.RenameTable(
                name: "TrainStations",
                newName: "TrainStation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainStation",
                table: "TrainStation",
                column: "Id");
        }
    }
}

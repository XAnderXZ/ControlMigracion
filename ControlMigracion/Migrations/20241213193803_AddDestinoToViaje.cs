using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlMigracion.Migrations
{
    /// <inheritdoc />
    public partial class AddDestinoToViaje : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Destino",
                table: "Viaje",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destino",
                table: "Viaje");
        }
    }
}

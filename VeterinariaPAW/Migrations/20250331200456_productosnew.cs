using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinariaPAW.Migrations
{
    /// <inheritdoc />
    public partial class productosnew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FotoUrl",
                table: "Producto",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoUrl",
                table: "Producto");
        }
    }
}

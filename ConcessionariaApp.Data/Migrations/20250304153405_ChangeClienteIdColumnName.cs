using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcessionariaApp.Migrations
{
    /// <inheritdoc />
    public partial class ChangeClienteIdColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClienteID",
                table: "Clientes",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Clientes",
                newName: "ClienteID");
        }
    }
}

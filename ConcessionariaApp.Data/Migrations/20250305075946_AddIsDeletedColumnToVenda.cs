using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcessionariaApp.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedColumnToVenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Vendas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Vendas");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcessionariaApp.Migrations
{
    /// <inheritdoc />
    public partial class Updates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProtocoloVenda",
                table: "Vendas",
                newName: "Protocolo");

            migrationBuilder.RenameColumn(
                name: "VendaID",
                table: "Vendas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TipoVeiculo",
                table: "Veiculos",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "VeiculoID",
                table: "Veiculos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "FabricanteID",
                table: "Fabricantes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Concessionarias",
                newName: "NomeConcessionaria");

            migrationBuilder.RenameColumn(
                name: "ConcessionariaID",
                table: "Concessionarias",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Protocolo",
                table: "Vendas",
                newName: "ProtocoloVenda");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Vendas",
                newName: "VendaID");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Veiculos",
                newName: "TipoVeiculo");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Veiculos",
                newName: "VeiculoID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Fabricantes",
                newName: "FabricanteID");

            migrationBuilder.RenameColumn(
                name: "NomeConcessionaria",
                table: "Concessionarias",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Concessionarias",
                newName: "ConcessionariaID");
        }
    }
}

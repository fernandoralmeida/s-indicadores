using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CNPJ = table.Column<string>(type: "varchar(14)", nullable: true),
                    RazaoSocial = table.Column<string>(type: "varchar(255)", nullable: true),
                    NaturezaJuridica = table.Column<string>(type: "varchar(255)", nullable: true),
                    CapitalSocial = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PorteEmpresa = table.Column<string>(type: "varchar(2)", nullable: true),
                    IdentificadorMatrizFilial = table.Column<string>(type: "varchar(1)", nullable: true),
                    NomeFantasia = table.Column<string>(type: "varchar(255)", nullable: true),
                    SituacaoCadastral = table.Column<string>(type: "varchar(2)", nullable: true),
                    DataSituacaoCadastral = table.Column<string>(type: "varchar(8)", nullable: true),
                    DataInicioAtividade = table.Column<string>(type: "varchar(8)", nullable: true),
                    CnaeFiscalPrincipal = table.Column<string>(type: "varchar(7)", nullable: true),
                    CnaeDescricao = table.Column<string>(type: "varchar(255)", nullable: true),
                    Logradouro = table.Column<string>(type: "varchar(255)", nullable: true),
                    Numero = table.Column<string>(type: "varchar(10)", nullable: true),
                    Bairro = table.Column<string>(type: "varchar(255)", nullable: true),
                    CEP = table.Column<string>(type: "varchar(8)", nullable: true),
                    UF = table.Column<string>(type: "varchar(2)", nullable: true),
                    Municipio = table.Column<string>(type: "varchar(50)", nullable: true),
                    OpcaoSimples = table.Column<string>(type: "varchar(1)", nullable: true),
                    DataOpcaoSimples = table.Column<string>(type: "varchar(8)", nullable: true),
                    DataExclusaoSimples = table.Column<string>(type: "varchar(8)", nullable: true),
                    OpcaoMEI = table.Column<string>(type: "varchar(1)", nullable: true),
                    DataOpcaoMEI = table.Column<string>(type: "varchar(8)", nullable: true),
                    DataExclusaoMEI = table.Column<string>(type: "varchar(8)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Municipios",
                columns: table => new
                {
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Municipios");
        }
    }
}

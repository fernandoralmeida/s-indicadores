using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Geojson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeojsonGeometries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeojsonGeometries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeojsonProperties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: true),
                    Geocode = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeojsonProperties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeojsonCoordinates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    X = table.Column<double>(type: "float", nullable: false),
                    Y = table.Column<double>(type: "float", nullable: false),
                    MGeometryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeojsonCoordinates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeojsonCoordinates_GeojsonGeometries_MGeometryId",
                        column: x => x.MGeometryId,
                        principalTable: "GeojsonGeometries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GeojsonFeatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "varchar(255)", nullable: true),
                    GeometryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PropertiesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeojsonFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeojsonFeatures_GeojsonGeometries_GeometryId",
                        column: x => x.GeometryId,
                        principalTable: "GeojsonGeometries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GeojsonFeatures_GeojsonProperties_PropertiesId",
                        column: x => x.PropertiesId,
                        principalTable: "GeojsonProperties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeojsonCoordinates_MGeometryId",
                table: "GeojsonCoordinates",
                column: "MGeometryId");

            migrationBuilder.CreateIndex(
                name: "IX_GeojsonFeatures_GeometryId",
                table: "GeojsonFeatures",
                column: "GeometryId");

            migrationBuilder.CreateIndex(
                name: "IX_GeojsonFeatures_PropertiesId",
                table: "GeojsonFeatures",
                column: "PropertiesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeojsonCoordinates");

            migrationBuilder.DropTable(
                name: "GeojsonFeatures");

            migrationBuilder.DropTable(
                name: "GeojsonGeometries");

            migrationBuilder.DropTable(
                name: "GeojsonProperties");
        }
    }
}

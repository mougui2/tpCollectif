using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionMagasin.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Libelle = table.Column<string>(nullable: false),
                    SKU = table.Column<string>(nullable: false),
                    DateSortie = table.Column<DateTime>(nullable: false),
                    PrixInitial = table.Column<float>(nullable: false),
                    Poids = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Secteurs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secteurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Etageres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PoidsMaximum = table.Column<float>(nullable: false),
                    IdSecteur = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etageres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Etageres_Secteurs_IdSecteur",
                        column: x => x.IdSecteur,
                        principalTable: "Secteurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PositionMagasin",
                columns: table => new
                {
                    IdEtagere = table.Column<int>(nullable: false),
                    IdArticle = table.Column<int>(nullable: false),
                    Quantite = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionMagasin", x => new { x.IdEtagere, x.IdArticle });
                    table.ForeignKey(
                        name: "FK_PositionMagasin_Articles_IdArticle",
                        column: x => x.IdArticle,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PositionMagasin_Etageres_IdEtagere",
                        column: x => x.IdEtagere,
                        principalTable: "Etageres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Etageres_IdSecteur",
                table: "Etageres",
                column: "IdSecteur");

            migrationBuilder.CreateIndex(
                name: "IX_PositionMagasin_IdArticle",
                table: "PositionMagasin",
                column: "IdArticle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PositionMagasin");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Etageres");

            migrationBuilder.DropTable(
                name: "Secteurs");
        }
    }
}

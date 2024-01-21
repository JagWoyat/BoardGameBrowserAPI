using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGameBrowserAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoardGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Rating = table.Column<double>(type: "REAL", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    MinPlayers = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxPlayers = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayingTime = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Designers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoardGameCategory",
                columns: table => new
                {
                    BoardGamesId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoriesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGameCategory", x => new { x.BoardGamesId, x.CategoriesId });
                    table.ForeignKey(
                        name: "FK_BoardGameCategory_BoardGames_BoardGamesId",
                        column: x => x.BoardGamesId,
                        principalTable: "BoardGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardGameCategory_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoardGameDesigner",
                columns: table => new
                {
                    BoardGamesId = table.Column<int>(type: "INTEGER", nullable: false),
                    DesignersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGameDesigner", x => new { x.BoardGamesId, x.DesignersId });
                    table.ForeignKey(
                        name: "FK_BoardGameDesigner_BoardGames_BoardGamesId",
                        column: x => x.BoardGamesId,
                        principalTable: "BoardGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardGameDesigner_Designers_DesignersId",
                        column: x => x.DesignersId,
                        principalTable: "Designers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardGameCategory_CategoriesId",
                table: "BoardGameCategory",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardGameDesigner_DesignersId",
                table: "BoardGameDesigner",
                column: "DesignersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardGameCategory");

            migrationBuilder.DropTable(
                name: "BoardGameDesigner");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "BoardGames");

            migrationBuilder.DropTable(
                name: "Designers");
        }
    }
}

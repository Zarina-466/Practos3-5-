using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksService.Migrations
{
    /// <inheritdoc />
    public partial class deq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autors",
                columns: table => new
                {
                    Id_Autors = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autors", x => x.Id_Autors);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id_Genres = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id_Genres);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id_Books = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Autors = table.Column<int>(type: "int", nullable: false),
                    Id_Genre = table.Column<int>(type: "int", nullable: false),
                    AvailableCopies = table.Column<int>(type: "int", nullable: false),
                    YearOfPublication = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id_Books);
                    table.ForeignKey(
                        name: "FK_Books_Autors_Id_Autors",
                        column: x => x.Id_Autors,
                        principalTable: "Autors",
                        principalColumn: "Id_Autors",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Genres_Id_Genre",
                        column: x => x.Id_Genre,
                        principalTable: "Genres",
                        principalColumn: "Id_Genres",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_Id_Autors",
                table: "Books",
                column: "Id_Autors");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Id_Genre",
                table: "Books",
                column: "Id_Genre");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Autors");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocBuilder.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doc_DocCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "DocCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocT_DocCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "DocCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllowMultiple = table.Column<bool>(type: "bit", nullable: false),
                    IsDropdown = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocItem_Doc_DocId",
                        column: x => x.DocId,
                        principalTable: "Doc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocItemT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocTId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllowMultiple = table.Column<bool>(type: "bit", nullable: false),
                    IsDropdown = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocItemT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocItemT_DocT_DocTId",
                        column: x => x.DocTId,
                        principalTable: "DocT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocItemId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocAnswer_DocItem_DocItemId",
                        column: x => x.DocItemId,
                        principalTable: "DocItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocItemId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Selected = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocOption_DocItem_DocItemId",
                        column: x => x.DocItemId,
                        principalTable: "DocItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocOptionT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocItemTId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocOptionT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocOptionT_DocItemT_DocItemTId",
                        column: x => x.DocItemTId,
                        principalTable: "DocItemT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doc_CategoryId",
                table: "Doc",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DocAnswer_DocItemId",
                table: "DocAnswer",
                column: "DocItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocItem_DocId",
                table: "DocItem",
                column: "DocId");

            migrationBuilder.CreateIndex(
                name: "IX_DocItemT_DocTId",
                table: "DocItemT",
                column: "DocTId");

            migrationBuilder.CreateIndex(
                name: "IX_DocOption_DocItemId",
                table: "DocOption",
                column: "DocItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DocOptionT_DocItemTId",
                table: "DocOptionT",
                column: "DocItemTId");

            migrationBuilder.CreateIndex(
                name: "IX_DocT_CategoryId",
                table: "DocT",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocAnswer");

            migrationBuilder.DropTable(
                name: "DocOption");

            migrationBuilder.DropTable(
                name: "DocOptionT");

            migrationBuilder.DropTable(
                name: "DocItem");

            migrationBuilder.DropTable(
                name: "DocItemT");

            migrationBuilder.DropTable(
                name: "Doc");

            migrationBuilder.DropTable(
                name: "DocT");

            migrationBuilder.DropTable(
                name: "DocCategory");
        }
    }
}

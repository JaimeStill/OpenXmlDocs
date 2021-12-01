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
                name: "TDoc",
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
                    table.PrimaryKey("PK_TDoc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TDoc_DocCategory_CategoryId",
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
                name: "TDocItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TDocId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllowMultiple = table.Column<bool>(type: "bit", nullable: false),
                    IsDropdown = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TDocItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TDocItem_TDoc_TDocId",
                        column: x => x.TDocId,
                        principalTable: "TDoc",
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
                name: "TDocOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TDocItemId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TDocOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TDocOption_TDocItem_TDocItemId",
                        column: x => x.TDocItemId,
                        principalTable: "TDocItem",
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
                name: "IX_DocOption_DocItemId",
                table: "DocOption",
                column: "DocItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TDoc_CategoryId",
                table: "TDoc",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TDocItem_TDocId",
                table: "TDocItem",
                column: "TDocId");

            migrationBuilder.CreateIndex(
                name: "IX_TDocOption_TDocItemId",
                table: "TDocOption",
                column: "TDocItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocAnswer");

            migrationBuilder.DropTable(
                name: "DocOption");

            migrationBuilder.DropTable(
                name: "TDocOption");

            migrationBuilder.DropTable(
                name: "DocItem");

            migrationBuilder.DropTable(
                name: "TDocItem");

            migrationBuilder.DropTable(
                name: "Doc");

            migrationBuilder.DropTable(
                name: "TDoc");

            migrationBuilder.DropTable(
                name: "DocCategory");
        }
    }
}

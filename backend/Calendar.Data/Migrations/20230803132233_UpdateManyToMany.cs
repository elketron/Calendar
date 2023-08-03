using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calendar.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryTodoItem");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "TodoItemCategory",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "integer", nullable: false),
                    TodoItemsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItemCategory", x => new { x.CategoriesId, x.TodoItemsId });
                    table.ForeignKey(
                        name: "FK_TodoItemCategory_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TodoItemCategory_TodoItems_TodoItemsId",
                        column: x => x.TodoItemsId,
                        principalTable: "TodoItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoItemCategory_TodoItemsId",
                table: "TodoItemCategory",
                column: "TodoItemsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoItemCategory");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Categories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CategoryTodoItem",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "integer", nullable: false),
                    ParentsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTodoItem", x => new { x.CategoriesId, x.ParentsId });
                    table.ForeignKey(
                        name: "FK_CategoryTodoItem_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryTodoItem_TodoItems_ParentsId",
                        column: x => x.ParentsId,
                        principalTable: "TodoItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTodoItem_ParentsId",
                table: "CategoryTodoItem",
                column: "ParentsId");
        }
    }
}

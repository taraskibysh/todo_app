using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todo.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CreateRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TodoItemId",
                table: "Steps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Steps_TodoItemId",
                table: "Steps",
                column: "TodoItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_TodoItems_TodoItemId",
                table: "Steps",
                column: "TodoItemId",
                principalTable: "TodoItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Steps_TodoItems_TodoItemId",
                table: "Steps");

            migrationBuilder.DropIndex(
                name: "IX_Steps_TodoItemId",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "TodoItemId",
                table: "Steps");
        }
    }
}

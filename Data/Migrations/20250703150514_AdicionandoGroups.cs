using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdLista",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "Posicao",
                table: "Card");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "List",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_List_GroupId",
                table: "List",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GroupId",
                table: "AspNetUsers",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Groups_GroupId",
                table: "AspNetUsers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_List_Groups_GroupId",
                table: "List",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Groups_GroupId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_List_Groups_GroupId",
                table: "List");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_List_GroupId",
                table: "List");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GroupId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "List");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "IdLista",
                table: "Card",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Posicao",
                table: "Card",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

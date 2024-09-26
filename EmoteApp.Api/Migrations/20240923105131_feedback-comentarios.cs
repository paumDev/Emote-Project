using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BethanysPieShopHRM.Api.Migrations
{
    public partial class feedbackcomentarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmoteChangeLog",
                table: "EmoteChangeLog");

            migrationBuilder.RenameTable(
                name: "EmoteChangeLog",
                newName: "EmoteChangeLogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmoteChangeLogs",
                table: "EmoteChangeLogs",
                column: "EmoteChangeLogId");

            migrationBuilder.CreateIndex(
                name: "IX_EmoteChangeLogs_EmoteId",
                table: "EmoteChangeLogs",
                column: "EmoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmoteChangeLogs_Emotes_EmoteId",
                table: "EmoteChangeLogs",
                column: "EmoteId",
                principalTable: "Emotes",
                principalColumn: "EmoteId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmoteChangeLogs_Emotes_EmoteId",
                table: "EmoteChangeLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmoteChangeLogs",
                table: "EmoteChangeLogs");

            migrationBuilder.DropIndex(
                name: "IX_EmoteChangeLogs_EmoteId",
                table: "EmoteChangeLogs");

            migrationBuilder.RenameTable(
                name: "EmoteChangeLogs",
                newName: "EmoteChangeLog");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmoteChangeLog",
                table: "EmoteChangeLog",
                column: "EmoteChangeLogId");
        }
    }
}

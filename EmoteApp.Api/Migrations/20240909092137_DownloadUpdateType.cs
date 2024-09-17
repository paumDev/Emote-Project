using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BethanysPieShopHRM.Api.Migrations
{
    public partial class DownloadUpdateType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "NumDownload",
                table: "Emotes",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Emotes",
                keyColumn: "EmoteId",
                keyValue: 1,
                column: "NumDownload",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Emotes",
                keyColumn: "EmoteId",
                keyValue: 2,
                column: "NumDownload",
                value: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NumDownload",
                table: "Emotes",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "Emotes",
                keyColumn: "EmoteId",
                keyValue: 1,
                column: "NumDownload",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Emotes",
                keyColumn: "EmoteId",
                keyValue: 2,
                column: "NumDownload",
                value: 0);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BethanysPieShopHRM.Api.Migrations
{
    public partial class CDatabase1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JoinedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Emotes",
                columns: table => new
                {
                    EmoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emotes", x => x.EmoteId);
                    table.ForeignKey(
                        name: "FK_Emotes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "JoinedDate", "UserName" },
                values: new object[,]
                {
                    { 1, null, "JuanitoXD3234542" },
                    { 2, null, "Jorge353424234" },
                    { 3, null, "Abascalterrorista" },
                    { 4, null, "gato_vietnamita" },
                    { 5, null, "jose:D" },
                    { 6, null, "chinchon" },
                    { 7, null, "bobooklk" },
                    { 8, null, "miguelin_de_murcia" },
                    { 9, null, "nayara_rivas_who" }
                });

            migrationBuilder.InsertData(
                table: "Emotes",
                columns: new[] { "EmoteId", "Approved", "CreationDate", "Description", "Height", "Name", "Status", "UserId", "Version", "Weight", "Width" },
                values: new object[] { 1, true, new DateTime(2023, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "the poor coballa this one regrets what he has done", 128, "oh", 0, 1, 1.0, "160KB", 128 });

            migrationBuilder.InsertData(
                table: "Emotes",
                columns: new[] { "EmoteId", "Approved", "CreationDate", "Description", "Height", "Name", "Status", "UserId", "Version", "Weight", "Width" },
                values: new object[] { 2, true, new DateTime(2023, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "this freak cat laughs at you resoundingly", 96, "LMAO", 1, 2, 1.2, "29KB", 384 });

            migrationBuilder.CreateIndex(
                name: "IX_Emotes_UserId",
                table: "Emotes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emotes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

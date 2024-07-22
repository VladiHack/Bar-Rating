using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bar_rating.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    BarImage = table.Column<byte[]>(type: "image", nullable: true),
                    Name = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true),
                    Password = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true),
                    FirstName = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true),
                    LastName = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    BarId = table.Column<int>(type: "int", nullable: true),
                    ReviewText = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Review__BarId__5165187F",
                        column: x => x.BarId,
                        principalTable: "Bar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Review__UserId__5070F446",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Review_BarId",
                table: "Review",
                column: "BarId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserId",
                table: "Review",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Bar");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

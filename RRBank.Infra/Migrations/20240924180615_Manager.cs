using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRBank.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Manager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Client",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    LastName = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    Document = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    Age = table.Column<int>(type: "INT", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    Register = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    IsActive = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_ManagerId",
                table: "Client",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Manager_Clients",
                table: "Client",
                column: "ManagerId",
                principalTable: "Manager",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Manager_Clients",
                table: "Client");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropIndex(
                name: "IX_Client_ManagerId",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Client");
        }
    }
}

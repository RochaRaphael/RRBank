using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRBank.Infra.Migrations
{
    /// <inheritdoc />
    public partial class NewTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    LastName = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    Document = table.Column<string>(type: "VARCHAR(11)", maxLength: 11, nullable: false),
                    Age = table.Column<int>(type: "INT", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    Register = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CelNumber = table.Column<string>(type: "VARCHAR(14)", maxLength: 14, nullable: false),
                    IsActive = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manager_Clients",
                        column: x => x.ManagerId,
                        principalTable: "Manager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "VARCHAR(11)", maxLength: 11, nullable: false),
                    Balance = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    IsActive = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Client_Accounts",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestCancellation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Document = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestCancellation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestCancellation_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_ClientId",
                table: "Account",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_ManagerId",
                table: "Client",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestCancellation_ClientId",
                table: "RequestCancellation",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "RequestCancellation");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Manager");
        }
    }
}

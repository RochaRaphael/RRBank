using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRBank.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CelNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CelNumber",
                table: "Client",
                type: "VARCHAR(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CelNumber",
                table: "Client");
        }
    }
}

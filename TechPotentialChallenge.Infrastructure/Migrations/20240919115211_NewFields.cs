using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechPotentialChallenge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CPFSaler",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmailSaler",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TelephoneSeler",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPFSaler",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "EmailSaler",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TelephoneSeler",
                table: "Orders");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Codebridge.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Naming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "Dogs",
                newName: "weight");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "Dogs",
                newName: "color");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Dogs",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "TailLength",
                table: "Dogs",
                newName: "tail_length");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "weight",
                table: "Dogs",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "color",
                table: "Dogs",
                newName: "Color");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Dogs",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "tail_length",
                table: "Dogs",
                newName: "TailLength");
        }
    }
}

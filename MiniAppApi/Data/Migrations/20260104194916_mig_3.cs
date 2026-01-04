using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniAppApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LogoUrl",
                table: "Organizers",
                newName: "LogoImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LogoImageUrl",
                table: "Organizers",
                newName: "LogoUrl");
        }
    }
}

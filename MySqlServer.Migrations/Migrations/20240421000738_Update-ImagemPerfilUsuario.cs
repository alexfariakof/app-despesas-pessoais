using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MsMySqlServer.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImagemPerfilUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "ImagemPerfilUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "ImagemPerfilUsuario",
                type: "longtext",
                nullable: false);
        }
    }
}

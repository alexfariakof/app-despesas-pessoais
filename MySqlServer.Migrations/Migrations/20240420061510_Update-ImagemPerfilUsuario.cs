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
            migrationBuilder.AlterColumn<ushort>(
                name: "PerfilUsuario",
                table: "Usuario",
                type: "smallint unsigned",
                nullable: false,
                defaultValue: (ushort)2,
                oldClrType: typeof(ushort),
                oldType: "smallint unsigned");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "ImagemPerfilUsuario",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "ImagemPerfilUsuario");

            migrationBuilder.AlterColumn<ushort>(
                name: "PerfilUsuario",
                table: "Usuario",
                type: "smallint unsigned",
                nullable: false,
                oldClrType: typeof(ushort),
                oldType: "smallint unsigned",
                oldDefaultValue: (ushort)2);
        }
    }
}

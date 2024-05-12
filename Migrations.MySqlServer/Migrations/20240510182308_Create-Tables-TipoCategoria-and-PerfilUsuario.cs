using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MsMySqlServer.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class CreateTablesTipoCategoriaandPerfilUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerfilUsuario_Usuario_UsuarioId",
                table: "PerfilUsuario");

            migrationBuilder.DropIndex(
                name: "IX_PerfilUsuario_UsuarioId",
                table: "PerfilUsuario");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "PerfilUsuario");

            migrationBuilder.AddColumn<int>(
                name: "PerfilUsuarioId",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_PerfilUsuarioId",
                table: "Usuario",
                column: "PerfilUsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_PerfilUsuario_PerfilUsuarioId",
                table: "Usuario",
                column: "PerfilUsuarioId",
                principalTable: "PerfilUsuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_PerfilUsuario_PerfilUsuarioId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_PerfilUsuarioId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "PerfilUsuarioId",
                table: "Usuario");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "PerfilUsuario",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "PerfilUsuario",
                keyColumn: "Id",
                keyValue: 1,
                column: "UsuarioId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PerfilUsuario",
                keyColumn: "Id",
                keyValue: 2,
                column: "UsuarioId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_PerfilUsuario_UsuarioId",
                table: "PerfilUsuario",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PerfilUsuario_Usuario_UsuarioId",
                table: "PerfilUsuario",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id");
        }
    }
}

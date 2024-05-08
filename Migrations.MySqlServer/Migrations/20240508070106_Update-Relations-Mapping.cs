using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MsMySqlServer.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationsMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesa_Usuario_UsuarioId",
                table: "Despesa");

            migrationBuilder.DropForeignKey(
                name: "FK_Lancamento_Usuario_UsuarioId",
                table: "Lancamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Receita_Usuario_UsuarioId",
                table: "Receita");

            migrationBuilder.AlterColumn<ushort>(
                name: "PerfilUsuario",
                table: "Usuario",
                type: "smallint unsigned",
                nullable: false,
                oldClrType: typeof(ushort),
                oldType: "smallint unsigned",
                oldDefaultValue: (ushort)2);

            migrationBuilder.AddForeignKey(
                name: "FK_Despesa_Usuario_UsuarioId",
                table: "Despesa",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lancamento_Usuario_UsuarioId",
                table: "Lancamento",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Receita_Usuario_UsuarioId",
                table: "Receita",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesa_Usuario_UsuarioId",
                table: "Despesa");

            migrationBuilder.DropForeignKey(
                name: "FK_Lancamento_Usuario_UsuarioId",
                table: "Lancamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Receita_Usuario_UsuarioId",
                table: "Receita");

            migrationBuilder.AlterColumn<ushort>(
                name: "PerfilUsuario",
                table: "Usuario",
                type: "smallint unsigned",
                nullable: false,
                defaultValue: (ushort)2,
                oldClrType: typeof(ushort),
                oldType: "smallint unsigned");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesa_Usuario_UsuarioId",
                table: "Despesa",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lancamento_Usuario_UsuarioId",
                table: "Lancamento",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receita_Usuario_UsuarioId",
                table: "Receita",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

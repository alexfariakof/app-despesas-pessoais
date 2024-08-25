using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations.MsSqlServer.Migrations.Application
{
    /// <inheritdoc />
    public partial class ChangeIdsTypeInttoUUID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categoria_TipoCategoria_TipoCategoriaId",
                table: "Categoria");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_PerfilUsuario_PerfilUsuarioId",
                table: "Usuario");

            migrationBuilder.AlterColumn<int>(
                name: "PerfilUsuarioId",
                table: "Usuario",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Id",
                table: "Usuario",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<byte[]>(
                name: "UsuarioId",
                table: "Receita",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<byte[]>(
                name: "CategoriaId",
                table: "Receita",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Id",
                table: "Receita",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<byte[]>(
                name: "UsuarioId",
                table: "Lancamento",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ReceitaId",
                table: "Lancamento",
                type: "binary(16)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "DespesaId",
                table: "Lancamento",
                type: "binary(16)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "CategoriaId",
                table: "Lancamento",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Id",
                table: "Lancamento",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<byte[]>(
                name: "UsuarioId",
                table: "ImagemPerfilUsuario",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Id",
                table: "ImagemPerfilUsuario",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<byte[]>(
                name: "UsuarioId",
                table: "Despesa",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<byte[]>(
                name: "CategoriaId",
                table: "Despesa",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Id",
                table: "Despesa",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<byte[]>(
                name: "UsuarioId",
                table: "ControleAcesso",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Id",
                table: "ControleAcesso",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<byte[]>(
                name: "UsuarioId",
                table: "Categoria",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TipoCategoriaId",
                table: "Categoria",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Id",
                table: "Categoria",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Categoria_TipoCategoria_TipoCategoriaId",
                table: "Categoria",
                column: "TipoCategoriaId",
                principalTable: "TipoCategoria",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_PerfilUsuario_PerfilUsuarioId",
                table: "Usuario",
                column: "PerfilUsuarioId",
                principalTable: "PerfilUsuario",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categoria_TipoCategoria_TipoCategoriaId",
                table: "Categoria");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_PerfilUsuario_PerfilUsuarioId",
                table: "Usuario");

            migrationBuilder.AlterColumn<int>(
                name: "PerfilUsuarioId",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Usuario",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Receita",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Receita",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Receita",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Lancamento",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<int>(
                name: "ReceitaId",
                table: "Lancamento",
                type: "int",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<int>(
                name: "DespesaId",
                table: "Lancamento",
                type: "int",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Lancamento",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Lancamento",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "ImagemPerfilUsuario",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ImagemPerfilUsuario",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Despesa",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Despesa",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Despesa",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "ControleAcesso",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ControleAcesso",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Categoria",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<int>(
                name: "TipoCategoriaId",
                table: "Categoria",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Categoria",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Categoria_TipoCategoria_TipoCategoriaId",
                table: "Categoria",
                column: "TipoCategoriaId",
                principalTable: "TipoCategoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_PerfilUsuario_PerfilUsuarioId",
                table: "Usuario",
                column: "PerfilUsuarioId",
                principalTable: "PerfilUsuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

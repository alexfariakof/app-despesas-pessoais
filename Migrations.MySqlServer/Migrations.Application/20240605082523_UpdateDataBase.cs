using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations.MySqlServer.Migrations.Application
{
    /// <inheritdoc />
    public partial class UpdateDataBase : Migration
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "Receita",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "Now()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "Lancamento",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "Now()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "Lancamento",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataVencimento",
                table: "Despesa",
                type: "timestamp",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "Despesa",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "Now()");

            migrationBuilder.AlterColumn<int>(
                name: "TipoCategoriaId",
                table: "Categoria",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "Receita",
                type: "datetime",
                nullable: false,
                defaultValueSql: "Now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "Lancamento",
                type: "datetime",
                nullable: false,
                defaultValueSql: "Now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "Lancamento",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataVencimento",
                table: "Despesa",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "Despesa",
                type: "datetime",
                nullable: false,
                defaultValueSql: "Now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<int>(
                name: "TipoCategoriaId",
                table: "Categoria",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

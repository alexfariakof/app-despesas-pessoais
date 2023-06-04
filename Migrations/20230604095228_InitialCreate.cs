using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace despesas_backend_api_net_core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    SobreNome = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Telefone = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    StatusUsuario = table.Column<ushort>(type: "smallint unsigned", nullable: false),
                    perfilUsuario = table.Column<ushort>(type: "smallint unsigned", nullable: false, defaultValue: (ushort)2)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    TipoCategoria = table.Column<ushort>(type: "smallint unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categoria_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ControleAcesso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Login = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Senha = table.Column<string>(type: "longtext", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControleAcesso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ControleAcesso_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PerfilFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerfilFile_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Despesa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValue: new DateTime(2023, 6, 4, 6, 52, 27, 954, DateTimeKind.Local).AddTicks(7796)),
                    Descricao = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despesa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Despesa_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Despesa_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Receita",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValue: new DateTime(2023, 6, 4, 6, 52, 27, 955, DateTimeKind.Local).AddTicks(1390)),
                    Descricao = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0m),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receita", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receita_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receita_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Lancamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    DespesaId = table.Column<int>(type: "int", nullable: true),
                    ReceitaId = table.Column<int>(type: "int", nullable: true),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lancamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lancamento_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lancamento_Despesa_DespesaId",
                        column: x => x.DespesaId,
                        principalTable: "Despesa",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lancamento_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lancamento_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_UsuarioId",
                table: "Categoria",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ControleAcesso_Login",
                table: "ControleAcesso",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ControleAcesso_UsuarioId",
                table: "ControleAcesso",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesa_CategoriaId",
                table: "Despesa",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesa_UsuarioId",
                table: "Despesa",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Lancamento_CategoriaId",
                table: "Lancamento",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Lancamento_DespesaId",
                table: "Lancamento",
                column: "DespesaId");

            migrationBuilder.CreateIndex(
                name: "IX_Lancamento_ReceitaId",
                table: "Lancamento",
                column: "ReceitaId");

            migrationBuilder.CreateIndex(
                name: "IX_Lancamento_UsuarioId",
                table: "Lancamento",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilFile_Name",
                table: "PerfilFile",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerfilFile_Url",
                table: "PerfilFile",
                column: "Url",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerfilFile_UsuarioId",
                table: "PerfilFile",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receita_CategoriaId",
                table: "Receita",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Receita_UsuarioId",
                table: "Receita",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Email",
                table: "Usuario",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControleAcesso");

            migrationBuilder.DropTable(
                name: "Lancamento");

            migrationBuilder.DropTable(
                name: "PerfilFile");

            migrationBuilder.DropTable(
                name: "Despesa");

            migrationBuilder.DropTable(
                name: "Receita");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}

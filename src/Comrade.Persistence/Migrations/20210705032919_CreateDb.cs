using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Comrade.Persistence.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AIRP_AIRPLANE",
                columns: table => new
                {
                    AIRP_SQ_AIRPLANE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AIRP_TX_CODIGO = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    AIRP_TX_MODELO = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    AIRP_QT_PASSAGEIRO = table.Column<int>(type: "int", nullable: false),
                    AIRP_DT_REGISTRO = table.Column<string>(type: "varchar(48)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIRP_AIRPLANE", x => x.AIRP_SQ_AIRPLANE);
                });

            migrationBuilder.CreateTable(
                name: "USSI_USUARIO_SISTEMA",
                columns: table => new
                {
                    USSI_SQ_USUARIO_SISTEMA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USSI_TX_NOME = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    USSI_TX_EMAIL = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    USSI_TX_SENHA = table.Column<string>(type: "varchar(1023)", maxLength: 1023, nullable: false),
                    USSI_TX_MATRICULA = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    USSI_DT_REGISTRO = table.Column<string>(type: "varchar(48)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USSI_USUARIO_SISTEMA", x => x.USSI_SQ_USUARIO_SISTEMA);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AIRPLANE_CODIGO",
                table: "AIRP_AIRPLANE",
                column: "AIRP_TX_CODIGO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_SISTEMA_EMAIL",
                table: "USSI_USUARIO_SISTEMA",
                column: "USSI_TX_EMAIL",
                unique: true,
                filter: "[USSI_TX_EMAIL] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_SISTEMA_MATRICULA",
                table: "USSI_USUARIO_SISTEMA",
                column: "USSI_TX_MATRICULA",
                unique: true);

            var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Migrations",
                "20210705032919_CreateDb_Up.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFile));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AIRP_AIRPLANE");

            migrationBuilder.DropTable(
                name: "USSI_USUARIO_SISTEMA");
        }
    }
}

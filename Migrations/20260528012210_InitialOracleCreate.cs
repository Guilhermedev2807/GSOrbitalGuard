using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrbitalGuard.NETGS.Migrations
{
    /// <inheritdoc />
    public partial class InitialOracleCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_OG_CIDADE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: false),
                    RiscoAtual = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_OG_CIDADE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_OG_SENSOR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Tipo = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Localizacao = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CidadeId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_OG_SENSOR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_OG_SENSOR_TB_OG_CIDADE_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "TB_OG_CIDADE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_OG_SENSOR_CidadeId",
                table: "TB_OG_SENSOR",
                column: "CidadeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_OG_SENSOR");

            migrationBuilder.DropTable(
                name: "TB_OG_CIDADE");
        }
    }
}

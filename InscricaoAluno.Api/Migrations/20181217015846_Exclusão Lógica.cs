using Microsoft.EntityFrameworkCore.Migrations;

namespace InscricaoAluno.Api.Migrations
{
    public partial class ExclusãoLógica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                table: "Inscricao",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                table: "Curso",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                table: "Aluno",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Excluido",
                table: "Inscricao");

            migrationBuilder.DropColumn(
                name: "Excluido",
                table: "Curso");

            migrationBuilder.DropColumn(
                name: "Excluido",
                table: "Aluno");
        }
    }
}

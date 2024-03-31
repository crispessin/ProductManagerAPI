using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.Data.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "produto",
                columns: new[] { "idproduto", "nome", "preco", "estoque" },
                values: new object[,]
                {
                    { 1, "Teclado", 1.1000000000000001, 10 },
                    { 2, "Monitor", 20.0, 20 },
                    { 3, "Mouse", 30.0, 30 },
                    { 4, "Fone", 30.0, 40 },
                    { 5, "Carregador", 30.0, 50 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "produto",
                keyColumn: "idproduto",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "produto",
                keyColumn: "idproduto",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "produto",
                keyColumn: "idproduto",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "produto",
                keyColumn: "idproduto",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "produto",
                keyColumn: "idproduto",
                keyValue: 5);
        }
    }
}

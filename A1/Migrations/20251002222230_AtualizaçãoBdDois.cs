using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace A1.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaçãoBdDois : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredienteItemCardapio_ItensCardapio_ItensCardapioId",
                table: "IngredienteItemCardapio");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItens_ItensCardapio_ItemCardapioId",
                table: "PedidoItens");

            migrationBuilder.DropForeignKey(
                name: "FK_SugestoesDiarias_ItensCardapio_ItemCardapioId",
                table: "SugestoesDiarias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItensCardapio",
                table: "ItensCardapio");

            migrationBuilder.RenameTable(
                name: "ItensCardapio",
                newName: "ItemCardapio");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemCardapio",
                table: "ItemCardapio",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredienteItemCardapio_ItemCardapio_ItensCardapioId",
                table: "IngredienteItemCardapio",
                column: "ItensCardapioId",
                principalTable: "ItemCardapio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItens_ItemCardapio_ItemCardapioId",
                table: "PedidoItens",
                column: "ItemCardapioId",
                principalTable: "ItemCardapio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SugestoesDiarias_ItemCardapio_ItemCardapioId",
                table: "SugestoesDiarias",
                column: "ItemCardapioId",
                principalTable: "ItemCardapio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredienteItemCardapio_ItemCardapio_ItensCardapioId",
                table: "IngredienteItemCardapio");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItens_ItemCardapio_ItemCardapioId",
                table: "PedidoItens");

            migrationBuilder.DropForeignKey(
                name: "FK_SugestoesDiarias_ItemCardapio_ItemCardapioId",
                table: "SugestoesDiarias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemCardapio",
                table: "ItemCardapio");

            migrationBuilder.RenameTable(
                name: "ItemCardapio",
                newName: "ItensCardapio");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItensCardapio",
                table: "ItensCardapio",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredienteItemCardapio_ItensCardapio_ItensCardapioId",
                table: "IngredienteItemCardapio",
                column: "ItensCardapioId",
                principalTable: "ItensCardapio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItens_ItensCardapio_ItemCardapioId",
                table: "PedidoItens",
                column: "ItemCardapioId",
                principalTable: "ItensCardapio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SugestoesDiarias_ItensCardapio_ItemCardapioId",
                table: "SugestoesDiarias",
                column: "ItemCardapioId",
                principalTable: "ItensCardapio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

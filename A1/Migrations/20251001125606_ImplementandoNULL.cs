using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace A1.Migrations
{
    /// <inheritdoc />
    public partial class ImplementandoNULL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carrinho_AspNetUsers_UsuarioId",
                table: "Carrinho");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemCardapio_Carrinho_CarrinhoId",
                table: "ItemCardapio");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensCarrinho_Carrinho_CarrinhoId",
                table: "ItensCarrinho");

            migrationBuilder.DropIndex(
                name: "IX_ItemCardapio_CarrinhoId",
                table: "ItemCardapio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carrinho",
                table: "Carrinho");

            migrationBuilder.DropColumn(
                name: "CarrinhoId",
                table: "ItemCardapio");

            migrationBuilder.RenameTable(
                name: "Carrinho",
                newName: "Carrinhos");

            migrationBuilder.RenameIndex(
                name: "IX_Carrinho_UsuarioId",
                table: "Carrinhos",
                newName: "IX_Carrinhos_UsuarioId");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carrinhos",
                table: "Carrinhos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carrinhos_AspNetUsers_UsuarioId",
                table: "Carrinhos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensCarrinho_Carrinhos_CarrinhoId",
                table: "ItensCarrinho",
                column: "CarrinhoId",
                principalTable: "Carrinhos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carrinhos_AspNetUsers_UsuarioId",
                table: "Carrinhos");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensCarrinho_Carrinhos_CarrinhoId",
                table: "ItensCarrinho");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carrinhos",
                table: "Carrinhos");

            migrationBuilder.RenameTable(
                name: "Carrinhos",
                newName: "Carrinho");

            migrationBuilder.RenameIndex(
                name: "IX_Carrinhos_UsuarioId",
                table: "Carrinho",
                newName: "IX_Carrinho_UsuarioId");

            migrationBuilder.AddColumn<int>(
                name: "CarrinhoId",
                table: "ItemCardapio",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carrinho",
                table: "Carrinho",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCardapio_CarrinhoId",
                table: "ItemCardapio",
                column: "CarrinhoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carrinho_AspNetUsers_UsuarioId",
                table: "Carrinho",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCardapio_Carrinho_CarrinhoId",
                table: "ItemCardapio",
                column: "CarrinhoId",
                principalTable: "Carrinho",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensCarrinho_Carrinho_CarrinhoId",
                table: "ItensCarrinho",
                column: "CarrinhoId",
                principalTable: "Carrinho",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

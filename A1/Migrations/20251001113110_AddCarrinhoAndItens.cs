using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace A1.Migrations
{
    /// <inheritdoc />
    public partial class AddCarrinhoAndItens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Usuario_UsuarioId",
                table: "Endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_Usuario_UsuarioId",
                table: "Reserva");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Reserva_UsuarioId",
                table: "Reserva");

            migrationBuilder.DropIndex(
                name: "IX_Endereco_UsuarioId",
                table: "Endereco");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "Reserva",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CarrinhoId",
                table: "ItemCardapio",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "ItemCardapio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "Endereco",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Carrinho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrinho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carrinho_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItensCarrinho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarrinhoId = table.Column<int>(type: "int", nullable: false),
                    ItemCardapioId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensCarrinho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensCarrinho_Carrinho_CarrinhoId",
                        column: x => x.CarrinhoId,
                        principalTable: "Carrinho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensCarrinho_ItemCardapio_ItemCardapioId",
                        column: x => x.ItemCardapioId,
                        principalTable: "ItemCardapio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_UsuarioId1",
                table: "Reserva",
                column: "UsuarioId1");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCardapio_CarrinhoId",
                table: "ItemCardapio",
                column: "CarrinhoId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_UsuarioId1",
                table: "Endereco",
                column: "UsuarioId1");

            migrationBuilder.CreateIndex(
                name: "IX_Carrinho_UsuarioId",
                table: "Carrinho",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensCarrinho_CarrinhoId",
                table: "ItensCarrinho",
                column: "CarrinhoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensCarrinho_ItemCardapioId",
                table: "ItensCarrinho",
                column: "ItemCardapioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_AspNetUsers_UsuarioId1",
                table: "Endereco",
                column: "UsuarioId1",
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
                name: "FK_Reserva_AspNetUsers_UsuarioId1",
                table: "Reserva",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_AspNetUsers_UsuarioId1",
                table: "Endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemCardapio_Carrinho_CarrinhoId",
                table: "ItemCardapio");

            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_AspNetUsers_UsuarioId1",
                table: "Reserva");

            migrationBuilder.DropTable(
                name: "ItensCarrinho");

            migrationBuilder.DropTable(
                name: "Carrinho");

            migrationBuilder.DropIndex(
                name: "IX_Reserva_UsuarioId1",
                table: "Reserva");

            migrationBuilder.DropIndex(
                name: "IX_ItemCardapio_CarrinhoId",
                table: "ItemCardapio");

            migrationBuilder.DropIndex(
                name: "IX_Endereco_UsuarioId1",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Reserva");

            migrationBuilder.DropColumn(
                name: "CarrinhoId",
                table: "ItemCardapio");

            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "ItemCardapio");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_UsuarioId",
                table: "Reserva",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_UsuarioId",
                table: "Endereco",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Usuario_UsuarioId",
                table: "Endereco",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_Usuario_UsuarioId",
                table: "Reserva",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

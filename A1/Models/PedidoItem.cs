using System.ComponentModel.DataAnnotations.Schema;

namespace A1.Models
{
    public class PedidoItem
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }

        // Armazena o preço do item NO MOMENTO da compra.
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecoUnitario { get; set; }

        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        public int ItemCardapioId { get; set; }
        public ItemCardapio ItemCardapio { get; set; }
    }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace A1.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Status { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorTotal { get; set; }

        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int AtendimentoId { get; set; }
        public Atendimento Atendimento { get; set; }

        // Um pedido pode ter vários itens
        public List<PedidoItem> Itens { get; set; } = new List<PedidoItem>();
    }
}
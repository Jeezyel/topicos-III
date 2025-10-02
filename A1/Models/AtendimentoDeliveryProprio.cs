using System.ComponentModel.DataAnnotations.Schema;

namespace A1.Models
{
    public class AtendimentoDeliveryProprio : Atendimento
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxaDeEntregaFixa { get; set; }
    }
}
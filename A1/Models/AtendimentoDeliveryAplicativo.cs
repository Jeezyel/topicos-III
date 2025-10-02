using System.ComponentModel.DataAnnotations.Schema;

namespace A1.Models
{
    public class AtendimentoDeliveryAplicativo : Atendimento
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal ComissaoParceiro { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxaParceiro { get; set; }
        public int ParceiroAppId { get; set; }
        public ParceiroApp ParceiroApp { get; set; }
    }
}
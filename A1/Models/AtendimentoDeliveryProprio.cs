using A1.Models;
using System.ComponentModel.DataAnnotations.Schema; // Adicione este using

public class AtendimentoDeliveryProprio : Atendimento
{
    [Column(TypeName = "decimal(18,2)")] // Boa prática para o banco de dados
    public decimal TaxaDeEntregaFixa { get; set; }
}
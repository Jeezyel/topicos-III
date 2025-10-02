namespace A1.Models
{
    public class ParceiroApp
    {
        public int Id { get; set; }
        public string NomeApp { get; set; } // Ex: iFood, AppX, etc.
        public List<AtendimentoDeliveryAplicativo> Atendimentos { get; set; } = new List<AtendimentoDeliveryAplicativo>();
    }
}
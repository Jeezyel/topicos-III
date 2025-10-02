namespace A1.Models
{
    public abstract class Atendimento
    {
        public int Id { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public Pedido Pedido { get; set; }
    }
}
namespace A1.Models
{
    public class SugestaoDiaria
    {
        public int Id { get; set; }

        public DateOnly Data { get; set; }
        public Periodo Periodo { get; set; }

        public int ItemCardapioId { get; set; }
        public ItemCardapio ItemCardapio { get; set; }
    }
}
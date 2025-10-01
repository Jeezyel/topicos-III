namespace A1.Models
{
    public enum Periodo
    {
        Almoco,
        Jantar
    }

    public class ItemCardapio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double PrecoBase { get; set; }
        public Periodo Periodo { get; set; }
        public int Quantidade { get; set; }

        // Propriedade de navegação para o relacionamento N-N com Ingrediente
        public ICollection<Ingrediente> Ingredientes { get; set; } = new List<Ingrediente>();
    }
}

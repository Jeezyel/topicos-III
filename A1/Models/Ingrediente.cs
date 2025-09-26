namespace A1.Models
{
    public class Ingrediente
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ICollection<ItemCardapio> ItensCardapio { get; set; } = new List<ItemCardapio>();
    }
}

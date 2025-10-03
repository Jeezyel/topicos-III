namespace A1.Models
{
    public class Ingrediente
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ICollection<ItemIngrediente> ItemIngredientes { get; set; } = new List<ItemIngrediente>();
    }
}

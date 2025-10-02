namespace A1.Models
{
    public class IngredienteViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = default!;
        public bool Selecionado { get; set; } // Propriedade para controlar se o checkbox está marcado
    }
}
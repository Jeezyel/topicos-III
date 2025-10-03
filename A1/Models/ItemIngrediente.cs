namespace A1.Models
{
    // Esta classe representa a tabela de junção entre ItemCardapio e Ingrediente
    public class ItemIngrediente
    {
        // Chaves estrangeiras que, juntas, formarão a chave primária composta
        public int ItemCardapioId { get; set; }
        public int IngredienteId { get; set; }

        // Propriedades de navegação para as entidades relacionadas
        public ItemCardapio ItemCardapio { get; set; } = default!;
        public Ingrediente Ingrediente { get; set; } = default!;
    }
}
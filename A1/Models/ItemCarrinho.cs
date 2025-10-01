namespace A1.Models
{
    public class ItemCarrinho
    {
        public int Id { get; set; }

        public int CarrinhoId { get; set; }
        public Carrinho Carrinho { get; set; }

        public int ItemCardapioId { get; set; }
        public ItemCardapio ItemCardapio { get; set; }

        public int Quantidade { get; set; }
    }
}

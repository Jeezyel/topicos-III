namespace A1.Models
{
    public class Carrinho
    {

        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<ItemCarrinho> Itens { get; set; } = new List<ItemCarrinho>();


        public Carrinho() { }

        public Carrinho(Usuario usuario)
        {
            Usuario = usuario;
        }

        public int QantidadeItens()
        {
            return Itens.Count;
        }
    }
}

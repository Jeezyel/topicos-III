using A1.Data;
using A1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A1.Extensions;

// Verifique se o namespace aqui corresponde ao do seu projeto
namespace A1.Pages.Carrinho
{
    // Classe auxiliar para exibir os dados na página do carrinho
    public class ItemCarrinhoViewModel
    {
        public int ItemCardapioId { get; set; }
        public string Nome { get; set; }
        public double PrecoUnitario { get; set; }
        public int Quantidade { get; set; }
        public double Subtotal => PrecoUnitario * Quantidade;
    }

    // O nome da classe deve ser o nome do seu arquivo + Model
    public class CarrinhoPageModel : PageModel
    {
        private readonly A1Context _context;

        public CarrinhoPageModel(A1Context context)
        {
            _context = context;
        }

        public List<ItemCarrinhoViewModel> Carrinho { get; set; } = new();
        public double TotalCarrinho { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // A lógica para ler o carrinho da sessão e buscar os dados no banco
            var carrinhoDaSessao = HttpContext.Session.Get<List<ItemCarrinho>>("Carrinho");

            if (carrinhoDaSessao != null && carrinhoDaSessao.Any())
            {
                foreach (var item in carrinhoDaSessao)
                {
                    var itemCardapio = await _context.ItensCardapio.FindAsync(item.ItemCardapioId);
                    if (itemCardapio != null)
                    {
                        Carrinho.Add(new ItemCarrinhoViewModel
                        {
                            ItemCardapioId = item.ItemCardapioId,
                            Nome = itemCardapio.Nome,
                            PrecoUnitario = itemCardapio.PrecoBase,
                            Quantidade = item.Quantidade
                        });
                    }
                }
                TotalCarrinho = Carrinho.Sum(item => item.Subtotal);
            }
            return Page();
        }
    }
}
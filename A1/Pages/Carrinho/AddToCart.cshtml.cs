using A1.Data;
using A1.Extensions;
using A1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1.Pages.Carrinho
{
    public class AddToCartModel : PageModel
    {
        private readonly A1Context _context;

        public AddToCartModel(A1Context context)
        {
            _context = context;
        }

        // Este é o único handler que a página deve ter.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var itemExiste = await _context.ItensCardapio.AnyAsync(i => i.Id == id);
            if (!itemExiste)
            {
                return RedirectToPage("/Index");
            }

            var carrinho = HttpContext.Session.Get<List<ItemCarrinho>>("Carrinho") ?? new List<ItemCarrinho>();

            var itemNoCarrinho = carrinho.FirstOrDefault(i => i.ItemCardapioId == id);
            if (itemNoCarrinho == null)
            {
                carrinho.Add(new ItemCarrinho { ItemCardapioId = id, Quantidade = 1 });
            }
            else
            {
                itemNoCarrinho.Quantidade++;
            }

            HttpContext.Session.Set("Carrinho", carrinho);

            return RedirectToPage("/Menu");
        }
    }
}
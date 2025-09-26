using A1.Data;
using A1.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace A1.Pages.Cardapio
{
    public class IndexModel : PageModel
    {
        private readonly A1Context _context;

        public IndexModel(A1Context context)
        {
            _context = context;
        }

        public IList<ItemCardapio> ItensCardapio { get; set; }

        public async Task OnGetAsync()
        {
            // Busca todos os itens do cardápio no banco de dados e os armazena na lista
            ItensCardapio = await _context.ItemCardapio.ToListAsync();
        }
    }
}
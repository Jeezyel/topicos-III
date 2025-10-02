using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using A1.Data;
using A1.Models;

namespace A1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly A1.Data.A1Context _context;

        public IndexModel(A1.Data.A1Context context)
        {
            _context = context;
        }

        public IList<ItemCarrinho> ItemCarrinho { get;set; } = default!;
        public List<ItemCardapio> Cardapio { get; set; }

        /*public async Task OnGetAsync()
        {
            ItemCarrinho = await _context.ItensCarrinho
                .Include(i => i.Carrinho)
                .Include(i => i.ItemCardapio).ToListAsync();
        }*/

        public void OnGet()
        {
            // Aqui você pode buscar os itens do banco de dados
            Cardapio = _context.ItemCardapio.ToList(); // Supondo que você tenha um DbSet<ItemCardapio> no seu contexto
        }
    }
}
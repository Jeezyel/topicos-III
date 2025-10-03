using A1.Data;
using A1.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1.Pages
{
    public class MenuModel : PageModel
    {
        private readonly A1Context _context;

        public MenuModel(A1Context context)
        {
            _context = context;
        }

        // Propriedades para guardar os itens separados por período
        public IList<ItemCardapio> ItensAlmoco { get; set; }
        public IList<ItemCardapio> ItensJantar { get; set; }

        public async Task OnGetAsync()
        {
            // Busca todos os itens do cardápio, incluindo os ingredientes associados
            var todosOsItens = await _context.ItensCardapio
                                     .Include(i => i.ItemIngredientes)
                                     .ThenInclude(ii => ii.Ingrediente)
                                     .AsNoTracking()
                                     .ToListAsync();

            // Separa os itens em duas listas: uma para almoço e outra para jantar
            ItensAlmoco = todosOsItens.Where(i => i.Periodo == Periodo.Almoco).ToList();
            ItensJantar = todosOsItens.Where(i => i.Periodo == Periodo.Jantar).ToList();
        }
    }
}
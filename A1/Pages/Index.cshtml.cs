using A1.Data;
using A1.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Adicionado para o ILogger
using System;
using System.Linq;
using System.Threading.Tasks;

namespace A1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly A1Context _context;

        public ItemCardapio? SugestaoAlmoco { get; private set; }
        public ItemCardapio? SugestaoJantar { get; private set; }

        // --- CONSTRUTOR CORRIGIDO ---
        // Agora ele recebe tanto o ILogger quanto o A1Context
        public IndexModel(ILogger<IndexModel> logger, A1Context context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnGetAsync()
        {
            // Pega a data de hoje para a consulta
            var hoje = DateOnly.FromDateTime(DateTime.Now);

            // Busca a sugestão do dia para o Almoço
            SugestaoAlmoco = await _context.SugestoesDiarias
                .Where(s => s.Data == hoje && s.Periodo == Periodo.Almoco)
                .Select(s => s.ItemCardapio)
                .FirstOrDefaultAsync();

            // Busca a sugestão do dia para o Jantar
            SugestaoJantar = await _context.SugestoesDiarias
                .Where(s => s.Data == hoje && s.Periodo == Periodo.Jantar)
                .Select(s => s.ItemCardapio)
                .FirstOrDefaultAsync();
        }

    }
}
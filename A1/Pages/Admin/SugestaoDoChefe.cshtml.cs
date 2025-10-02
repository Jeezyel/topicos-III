using A1.Data;
using A1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace A1.Pages.Admin
{
    // adicionar uma autorização para que apenas Admins acessem esta página no futuro.
    // [Authorize(Roles = "Admin")]
    public class SugestaoDoChefeModel : PageModel
    {
        private readonly A1Context _context;

        public SugestaoDoChefeModel(A1Context context)
        {
            _context = context;
        }

        public SelectList AlmocoOptions { get; set; }
        public SelectList JantarOptions { get; set; }

        [BindProperty]
        public int? AlmocoSelecionadoId { get; set; }
        [BindProperty]
        public int? JantarSelecionadoId { get; set; }

        public DateOnly DataDeHoje { get; set; }

        [TempData]
        public string MensagemSucesso { get; set; }

        public async Task OnGetAsync()
        {
            DataDeHoje = DateOnly.FromDateTime(DateTime.Now);

            var sugestoesDeHoje = await _context.SugestoesDiarias
                .Where(s => s.Data == DataDeHoje)
                .ToListAsync();

            var sugestaoAlmocoHoje = sugestoesDeHoje.FirstOrDefault(s => s.Periodo == Periodo.Almoco);
            AlmocoSelecionadoId = sugestaoAlmocoHoje?.ItemCardapioId;

            var sugestaoJantarHoje = sugestoesDeHoje.FirstOrDefault(s => s.Periodo == Periodo.Jantar);
            JantarSelecionadoId = sugestaoJantarHoje?.ItemCardapioId;

            AlmocoOptions = new SelectList(await _context.ItensCardapio.Where(i => i.Periodo == Periodo.Almoco).ToListAsync(), "Id", "Nome", AlmocoSelecionadoId);
            JantarOptions = new SelectList(await _context.ItensCardapio.Where(i => i.Periodo == Periodo.Jantar).ToListAsync(), "Id", "Nome", JantarSelecionadoId);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            DataDeHoje = DateOnly.FromDateTime(DateTime.Now);

            // --- Lógica para o Almoço ---
            var sugestaoAlmocoAtual = await _context.SugestoesDiarias
                .FirstOrDefaultAsync(s => s.Data == DataDeHoje && s.Periodo == Periodo.Almoco);

            if (AlmocoSelecionadoId.HasValue) // Se um prato foi selecionado
            {
                if (sugestaoAlmocoAtual != null) // Se já existia sugestão, atualiza
                {
                    sugestaoAlmocoAtual.ItemCardapioId = AlmocoSelecionadoId.Value;
                }
                else // Se não existia, cria uma nova
                {
                    _context.SugestoesDiarias.Add(new SugestaoDiaria { Data = DataDeHoje, Periodo = Periodo.Almoco, ItemCardapioId = AlmocoSelecionadoId.Value });
                }
            }
            else if (sugestaoAlmocoAtual != null) // Se NADA foi selecionado e existia uma sugestão, remove
            {
                _context.SugestoesDiarias.Remove(sugestaoAlmocoAtual);
            }

            // --- Lógica para o Jantar ---
            var sugestaoJantarAtual = await _context.SugestoesDiarias
                .FirstOrDefaultAsync(s => s.Data == DataDeHoje && s.Periodo == Periodo.Jantar);

            if (JantarSelecionadoId.HasValue) // Se um prato foi selecionado
            {
                if (sugestaoJantarAtual != null) // Se já existia, atualiza
                {
                    sugestaoJantarAtual.ItemCardapioId = JantarSelecionadoId.Value;
                }
                else // Se não existia, cria
                {
                    _context.SugestoesDiarias.Add(new SugestaoDiaria { Data = DataDeHoje, Periodo = Periodo.Jantar, ItemCardapioId = JantarSelecionadoId.Value });
                }
            }
            else if (sugestaoJantarAtual != null) // Se NADA foi selecionado e existia, remove
            {
                _context.SugestoesDiarias.Remove(sugestaoJantarAtual);
            }

            await _context.SaveChangesAsync();

            MensagemSucesso = "Sugestões salvas com sucesso!";

            return RedirectToPage();
        }
    }
}
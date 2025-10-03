using A1.Data;
using A1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1.Pages.Cardapio
{
    public class CreateModel : PageModel
    {
        private readonly A1Context _context;

        // Propriedade para o dropdown 'Período'
        public SelectList PeriodosOptions { get; set; }

        // Propriedade para os checkboxes de 'Ingredientes'
        [BindProperty]
        public List<IngredienteViewModel> IngredientesOptions { get; set; }

        [BindProperty]
        public ItemCardapio ItemCardapio { get; set; } = default!;

        public CreateModel(A1Context context)
        {
            _context = context;
            PeriodosOptions = new SelectList(Enum.GetValues(typeof(Periodo)));
        }

        public async Task<IActionResult> OnGetAsync()
        {
            IngredientesOptions = await _context.Ingredientes
                .Select(i => new IngredienteViewModel
                {
                    Id = i.Id,
                    Nome = i.Nome,
                    Selecionado = false
                }).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ItensCardapio.Add(ItemCardapio);

            foreach (var ingredienteVM in IngredientesOptions.Where(i => i.Selecionado))
            {
                // A única mudança é aqui. Em vez de adicionar o Ingrediente,
                // adicionamos um novo objeto ItemIngrediente.
                ItemCardapio.ItemIngredientes.Add(new ItemIngrediente { IngredienteId = ingredienteVM.Id });
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
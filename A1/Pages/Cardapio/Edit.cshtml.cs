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
    public class EditModel : PageModel
    {
        private readonly A1Context _context;

        public EditModel(A1Context context)
        {
            _context = context;
            PeriodosOptions = new SelectList(Enum.GetValues(typeof(Periodo)));
        }

        [BindProperty]
        public ItemCardapio ItemCardapio { get; set; } = default!;
        public SelectList PeriodosOptions { get; set; }

        [BindProperty]
        public List<IngredienteViewModel> IngredientesOptions { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // --- CORREÇÃO AQUI ---
            // Carrega o ItemCardapio, incluindo a entidade de junção e, a partir dela, o Ingrediente.
            ItemCardapio = await _context.ItensCardapio
                                 .Include(i => i.ItemIngredientes)
                                 .ThenInclude(ii => ii.Ingrediente)
                                 .AsNoTracking() // Boa prática para páginas de edição
                                 .FirstOrDefaultAsync(m => m.Id == id);

            if (ItemCardapio == null)
            {
                return NotFound();
            }

            var todosIngredientes = await _context.Ingredientes.ToListAsync();

            // --- CORREÇÃO AQUI ---
            // Pega os IDs dos ingredientes a partir da entidade de junção.
            var ingredientesDoPratoIds = new HashSet<int>(ItemCardapio.ItemIngredientes.Select(ii => ii.IngredienteId));

            IngredientesOptions = todosIngredientes.Select(ing => new IngredienteViewModel
            {
                Id = ing.Id,
                Nome = ing.Nome,
                Selecionado = ingredientesDoPratoIds.Contains(ing.Id)
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // --- CORREÇÃO AQUI ---
            // Carrega o prato original, incluindo a entidade de junção.
            var itemToUpdate = await _context.ItensCardapio
                                     .Include(i => i.ItemIngredientes)
                                     .FirstOrDefaultAsync(i => i.Id == id);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<ItemCardapio>(
                itemToUpdate,
                "ItemCardapio",
                i => i.Nome, i => i.Descricao, i => i.PrecoBase, i => i.Periodo))
            {
                UpdateItemIngredientes(itemToUpdate, IngredientesOptions);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }

        // Este método já estava correto na sua versão!
        private void UpdateItemIngredientes(ItemCardapio itemToUpdate, List<IngredienteViewModel> ingredientesOptions)
        {
            if (ingredientesOptions == null) return;

            var ingredientesSelecionadosIds = new HashSet<int>(
                ingredientesOptions.Where(i => i.Selecionado).Select(i => i.Id)
            );
            var ingredientesAtuais = new HashSet<int>(
                itemToUpdate.ItemIngredientes.Select(ii => ii.IngredienteId)
            );

            foreach (var ingrediente in _context.Ingredientes)
            {
                if (ingredientesSelecionadosIds.Contains(ingrediente.Id))
                {
                    if (!ingredientesAtuais.Contains(ingrediente.Id))
                    {
                        itemToUpdate.ItemIngredientes.Add(new ItemIngrediente { ItemCardapioId = itemToUpdate.Id, IngredienteId = ingrediente.Id });
                    }
                }
                else
                {
                    if (ingredientesAtuais.Contains(ingrediente.Id))
                    {
                        var itemIngredienteToRemove = itemToUpdate.ItemIngredientes.FirstOrDefault(ii => ii.IngredienteId == ingrediente.Id);
                        if (itemIngredienteToRemove != null)
                        {
                            _context.Remove(itemIngredienteToRemove);
                        }
                    }
                }
            }
        }
    }
}
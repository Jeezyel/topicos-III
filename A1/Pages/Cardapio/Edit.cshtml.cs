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

            // Carrega o ItemCardapio, INCLUINDO a lista de Ingredientes já associados a ele.
            ItemCardapio = await _context.ItensCardapio
                                 .Include(i => i.Ingredientes)
                                 .FirstOrDefaultAsync(m => m.Id == id);

            if (ItemCardapio == null)
            {
                return NotFound();
            }

            // Carrega a lista de TODOS os ingredientes do banco
            var todosIngredientes = await _context.Ingredientes.ToListAsync();

            // Pega os IDs dos ingredientes que JÁ ESTÃO associados a este prato.
            var ingredientesDoPratoIds = new HashSet<int>(ItemCardapio.Ingredientes.Select(i => i.Id));

            // Cria a lista de ViewModels para os checkboxes
            IngredientesOptions = todosIngredientes.Select(ing => new IngredienteViewModel
            {
                Id = ing.Id,
                Nome = ing.Nome,
                // Marca o checkbox como 'selecionado' se o ID do ingrediente estiver na lista de IDs do prato.
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

            // Carrega o prato original do banco, incluindo seus ingredientes atuais.
            var itemToUpdate = await _context.ItensCardapio
                                     .Include(i => i.Ingredientes)
                                     .FirstOrDefaultAsync(i => i.Id == id);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            // Atualiza as propriedades simples (Nome, Preço, etc.)
            if (await TryUpdateModelAsync<ItemCardapio>(
                itemToUpdate,
                "ItemCardapio", // Prefixo para os campos do formulário
                i => i.Nome, i => i.Descricao, i => i.PrecoBase, i => i.Periodo))
            {
                // Atualiza os ingredientes
                UpdateItemIngredientes(itemToUpdate, IngredientesOptions);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }

        private void UpdateItemIngredientes(ItemCardapio itemToUpdate, List<IngredienteViewModel> ingredientesOptions)
        {
            // Pega os IDs dos checkboxes que foram selecionados na tela
            var ingredientesSelecionadosIds = new HashSet<string>(
                ingredientesOptions.Where(i => i.Selecionado).Select(i => i.Id.ToString())
            );
            // Pega os IDs dos ingredientes que já estão associados ao prato no banco
            var ingredientesAtuaisIds = new HashSet<int>(
                itemToUpdate.Ingredientes.Select(i => i.Id)
            );

            foreach (var ingrediente in _context.Ingredientes)
            {
                // Se o checkbox foi marcado e o prato ainda não tem esse ingrediente, ADICIONA.
                if (ingredientesSelecionadosIds.Contains(ingrediente.Id.ToString()))
                {
                    if (!ingredientesAtuaisIds.Contains(ingrediente.Id))
                    {
                        itemToUpdate.Ingredientes.Add(ingrediente);
                    }
                }
                // Se o checkbox foi DESMARCADO e o prato tinha esse ingrediente, REMOVE.
                else
                {
                    if (ingredientesAtuaisIds.Contains(ingrediente.Id))
                    {
                        itemToUpdate.Ingredientes.Remove(ingrediente);
                    }
                }
            }
        }
    }
}
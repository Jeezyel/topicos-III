using A1.Data;
using A1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace A1.Pages.Cardapio
{
    public class EditModel : PageModel
    {
        private readonly A1Context _context;

        public EditModel(A1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public ItemCardapio ItemCardapio { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ItemCardapio = await _context.ItemCardapio.FirstOrDefaultAsync(m => m.Id == id);

            if (ItemCardapio == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ItemCardapio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ItemCardapio.Any(e => e.Id == ItemCardapio.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Redireciona para a página de listagem após salvar com sucesso
            return RedirectToPage("./Index");
        }
    }
}
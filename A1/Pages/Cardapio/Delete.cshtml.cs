using A1.Data;
using A1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace A1.Pages.Cardapio
{
    public class DeleteModel : PageModel
    {
        private readonly A1Context _context;

        public DeleteModel(A1Context context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ItemCardapio = await _context.ItemCardapio.FindAsync(id);

            if (ItemCardapio != null)
            {
                // Remove o item e salva as alterações
                _context.ItemCardapio.Remove(ItemCardapio);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
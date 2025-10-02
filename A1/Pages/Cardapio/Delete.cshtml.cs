using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using A1.Data;
using A1.Models;

namespace A1.Pages.Cardapio
{
    public class DeleteModel : PageModel
    {
        private readonly A1.Data.A1Context _context;

        public DeleteModel(A1.Data.A1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public ItemCardapio ItemCardapio { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemcardapio = await _context.ItensCardapio.FirstOrDefaultAsync(m => m.Id == id);

            if (itemcardapio == null)
            {
                return NotFound();
            }
            else
            {
                ItemCardapio = itemcardapio;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemcardapio = await _context.ItensCardapio.FindAsync(id);
            if (itemcardapio != null)
            {
                ItemCardapio = itemcardapio;
                _context.ItensCardapio.Remove(ItemCardapio);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

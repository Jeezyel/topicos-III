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
    public class DetailsModel : PageModel
    {
        private readonly A1.Data.A1Context _context;

        public DetailsModel(A1.Data.A1Context context)
        {
            _context = context;
        }

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
    }
}

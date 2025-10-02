using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using A1.Data;
using A1.Models;

namespace A1.Pages.Ingredientes
{
    public class IndexModel : PageModel
    {
        private readonly A1.Data.A1Context _context;

        public IndexModel(A1.Data.A1Context context)
        {
            _context = context;
        }

        public IList<Ingrediente> Ingrediente { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Ingrediente = await _context.Ingredientes.ToListAsync();
        }
    }
}

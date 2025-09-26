using A1.Data;
using A1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace A1.Pages.Cardapio
{
    public class CreateModel : PageModel
    {
        private readonly A1Context _context;

        public CreateModel(A1Context context)
        {
            _context = context;
        }

        // O OnGet apenas mostra a p�gina com o formul�rio vazio
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ItemCardapio ItemCardapio { get; set; }

        // O OnPostAsync � executado quando o formul�rio � enviado
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ItemCardapio.Add(ItemCardapio);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
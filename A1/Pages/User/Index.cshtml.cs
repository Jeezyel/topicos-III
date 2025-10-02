
using A1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace A1.Pages.User
{

    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly A1.Data.A1Context _context;

        public IndexModel(A1.Data.A1Context context)
        {
            _context = context;
        }

        public IList<Usuario> Usuario { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Usuario = await _context.Users.ToListAsync();
        }
    }
}

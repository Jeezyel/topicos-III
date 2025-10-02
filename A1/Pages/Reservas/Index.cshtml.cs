using A1.Data;
using A1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace A1.Pages.Reservas
{
    [Authorize] // Apenas usuários logados podem ver suas reservas
    public class IndexModel : PageModel
    {
        private readonly A1Context _context;

        public IndexModel(A1Context context)
        {
            _context = context;
        }

        public IList<Reserva> Reserva { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Reserva = await _context.Reservas
                .Where(r => r.UsuarioId == userId)
                .OrderByDescending(r => r.Data)
                .ToListAsync();
        }
    }
}
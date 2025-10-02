using A1.Data;
using A1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace A1.Pages.Reservas
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly A1.Data.A1Context _context;

        public CreateModel(A1.Data.A1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Reserva Reserva { get; set; } = new();

        [TempData]
        public string MensagemSucesso { get; set; }

        public void OnGet()
        {
            Reserva.Data = DateOnly.FromDateTime(DateTime.Now);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var hora = TimeOnly.FromTimeSpan(Reserva.Horario.ToTimeSpan());
            if (hora.Hour < 19 || hora.Hour >= 22)
            {
                ModelState.AddModelError("Reserva.Horario", "O horário da reserva deve ser entre 19:00 e 21:59.");
            }

            ModelState.Remove("Reserva.Usuario");
            ModelState.Remove("Reserva.Mesa");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Reserva.UsuarioId = userId;

            Reserva.CodigoConfirmacao = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

            _context.Reservas.Add(Reserva);
            await _context.SaveChangesAsync();

            MensagemSucesso = $"Reserva confirmada com sucesso! Seu código é: {Reserva.CodigoConfirmacao}";

            return RedirectToPage("./Create");
        }
    }
}
using A1.Data;
using A1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

// Classe que representa APENAS os dados que vêm do formulário
public class ReservaInputModel
{
    [Display(Name = "Data da Reserva")]
    [DataType(DataType.Date)]
    public DateOnly Data { get; set; }

    [Display(Name = "Horário")]
    [DataType(DataType.Time)]
    public TimeOnly Horario { get; set; }

    [Display(Name = "Número de Pessoas")]
    [Range(1, 100, ErrorMessage = "O número de pessoas deve ser no mínimo 1.")]
    public int NumeroPessoas { get; set; }
}

namespace A1.Pages.Reservas
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly A1Context _context;

        public CreateModel(A1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public ReservaInputModel Input { get; set; }

        [TempData]
        public string MensagemSucesso { get; set; }

        public void OnGet()
        {
            Input = new ReservaInputModel
            {
                Data = DateOnly.FromDateTime(DateTime.Now),
                NumeroPessoas = 2
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var hora = TimeOnly.FromTimeSpan(Input.Horario.ToTimeSpan());
            if (hora.Hour < 19 || hora.Hour >= 22)
            {
                ModelState.AddModelError("Input.Horario", "O horário da reserva deve ser entre 19:00 e 21:59.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var novaReserva = new Reserva
            {
                Data = Input.Data,
                Horario = Input.Horario,
                NumeroPessoas = Input.NumeroPessoas,
                UsuarioId = userId,
                CodigoConfirmacao = Guid.NewGuid().ToString().Substring(0, 8).ToUpper()
            };

            _context.Reservas.Add(novaReserva);
            await _context.SaveChangesAsync();

            MensagemSucesso = $"Reserva confirmada com sucesso! Seu código é: {novaReserva.CodigoConfirmacao}";

            return RedirectToPage();
        }
    }
}
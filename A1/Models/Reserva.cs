using System;
using System.ComponentModel.DataAnnotations;

namespace A1.Models
{
    public class Reserva
    {
        public int Id { get; set; }

        [Display(Name = "Data da Reserva")]
        [DataType(DataType.Date)]
        public DateOnly Data { get; set; }

        [Display(Name = "Horário")]
        [DataType(DataType.Time)]
        public TimeOnly Horario { get; set; }

        [Display(Name = "Número de Pessoas")]
        public int NumeroPessoas { get; set; }

        [Display(Name = "Código de Confirmação")]
        public string? CodigoConfirmacao { get; set; }

        // Relacionamentos
        public string UsuarioId { get; set; } = default!;

        [Display(Name = "Usuário")]
        public Usuario Usuario { get; set; } = default!;

        public int? MesaId { get; set; }
        public Mesa? Mesa { get; set; }
    }
}
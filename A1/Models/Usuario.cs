using Microsoft.AspNetCore.Identity;

namespace A1.Models
{
    public class Usuario : IdentityUser/**/
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        //public string Email { get; set; }
        public string? Senha { get; set; }
        public string? Telefone { get; set; }
        public List<Endereco>? Enderecos { get; set; } = new List<Endereco>();
        public List<Reserva>? Reservas { get; set; } = new List<Reserva>();
    }
}

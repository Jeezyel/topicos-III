using Microsoft.AspNetCore.Identity;

namespace A1.Models
{
    public class Usuario : IdentityUser
    {
        // O Id, UserName, Email, PhoneNumber, PasswordHash já existem em IdentityUser

        public string Nome { get; set; }

        public List<Endereco>? Enderecos { get; set; } = new List<Endereco>();
        public List<Reserva>? Reservas { get; set; } = new List<Reserva>();
        public List<Pedido>? Pedidos { get; set; } = new List<Pedido>();
    }
}
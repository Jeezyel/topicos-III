namespace A1.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime HorarioReserva { get; set; }
        public string Mesa { get; set; }

        public bool ValidarHorario(DateTime horario)
        {
            return horario.Hour >= 19 && horario.Hour <= 22;
        }
    }
}

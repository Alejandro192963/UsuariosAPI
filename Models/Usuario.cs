using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Models
{
    public class Usuario
    {
        [Key]
        public int ID { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
    }
}
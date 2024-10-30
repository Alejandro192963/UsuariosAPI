using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Models {
    public class Piso1Seccion2 {
        [Key]
        public int id { get; set; }
        public required string Cajon { get; set; }
        public required string Estado { get; set; }
        public string? Nombre { get; set; } // Cambiar a string? para permitir nulos
        public string? Email { get; set; } // 
    }
}
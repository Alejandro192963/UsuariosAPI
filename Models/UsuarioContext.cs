using Microsoft.EntityFrameworkCore;

namespace UsuariosApi.Models
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> options): base(options){}
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Piso1Seccion1> Piso1_Seccion1 { get; set; }
        public DbSet<Piso1Seccion2> Piso1_Seccion2 { get; set; }
    }
}
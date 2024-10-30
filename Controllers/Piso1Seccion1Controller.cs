using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsuariosApi.Models;

namespace UsuariosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Piso1_Seccion1Controller : ControllerBase
    {
        private readonly UsuarioContext _context;

        public Piso1_Seccion1Controller(UsuarioContext context)
        {
            _context = context;
        }

        // GET: api/Piso1_Seccion1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Piso1Seccion1>>> GetPiso1_Seccion1()
        {
            return await _context.Piso1_Seccion1.ToListAsync();
        }

        // GET: api/Piso1_Seccion1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Piso1Seccion1>> GetPiso1_Seccion1(int id)
        {
            var piso1_Seccion1 = await _context.Piso1_Seccion1.FindAsync(id);

            if (piso1_Seccion1 == null)
            {
                return NotFound();
            }

            return piso1_Seccion1;
        }

        // PUT: api/Piso1_Seccion1/actualizarEstado/5
// PUT: api/Piso1_Seccion1/actualizarEstado/5
[HttpPut("actualizarEstado/{id}")]
public async Task<IActionResult> UpdateEstado(int id, [FromBody] CajonUpdateRequest request, [FromQuery] string nombre, [FromQuery] string email)
{
    var cajon = await _context.Piso1_Seccion1.FindAsync(id);
    if (cajon == null)
    {
        return NotFound();
    }

    // Verificar si el usuario ya está ocupando un cajón
    var cajonOcupadoPorUsuario = await _context.Piso1_Seccion1
        .FirstOrDefaultAsync(c => c.Email == email && c.Estado == "Ocupado");

    if (cajonOcupadoPorUsuario != null && cajonOcupadoPorUsuario.id != id)
    {
        return BadRequest("Tu correo ya ocupa un cajón actualmente.");
    }

    // Alternar el estado del cajón
    if (cajon.Estado == "Disponible")
    {
        cajon.Estado = "Ocupado"; // Cambia a Ocupado
        cajon.Nombre = nombre; // Guardar el nombre del usuario
        cajon.Email = email; // Guardar el correo del usuario
    }
    else if (cajon.Estado == "Ocupado")
    {
        // Verificar si el usuario es el que ocupó el cajón
        if (cajon.Email != email)
        {
            return BadRequest("Tu no ocupas este cajón.");
        }

        cajon.Estado = "Disponible"; // Cambia a Disponible
        cajon.Nombre = null; // Limpiar el nombre si se libera el cajón
        cajon.Email = null; // Limpiar el correo si se libera el cajón
    }

    try
    {
        await _context.SaveChangesAsync();
        return NoContent(); // Código 204
    }
    catch (Exception ex)
    {
        // Loguear el error y retornar un código de error 500
        Console.WriteLine($"Error actualizando el estado del cajón: {ex.Message}");
        return StatusCode(500, "Ocurrió un error al actualizar el estado del cajón.");
    }
}



        // Clase para recibir el cuerpo de la solicitud
        public class CajonUpdateRequest
        {
            public required string Estado { get; set; }
        }

        // POST: api/Piso1_Seccion1
        [HttpPost]
        public async Task<ActionResult<Piso1Seccion1>> PostPiso1_Seccion1(Piso1Seccion1 piso1Seccion1)
        {
            _context.Piso1_Seccion1.Add(piso1Seccion1);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPiso1_Seccion1), new { id = piso1Seccion1.id }, piso1Seccion1);
        }

        // DELETE: api/Piso1_Seccion1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePiso1_Seccion1(int id)
        {
            var piso1_Seccion1 = await _context.Piso1_Seccion1.FindAsync(id);
            if (piso1_Seccion1 == null)
            {
                return NotFound();
            }

            _context.Piso1_Seccion1.Remove(piso1_Seccion1);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("resetearCajones")]
public async Task<IActionResult> RestablecerCajones()
{
    var cajones = await _context.Piso1_Seccion1.ToListAsync();
    foreach (var cajon in cajones)
    {
        cajon.Estado = "Disponible";
        cajon.Nombre = null;
        cajon.Email = null;
    }

    await _context.SaveChangesAsync();
    return NoContent(); // Código 204
}

        [HttpGet("disponibles")]
        public async Task<ActionResult<int>> GetDisponiblesCount()
        {
            int count = await _context.Piso1_Seccion1.CountAsync(p => p.Estado == "Disponible");
            return Ok(count);
        }

        private bool Piso1_Seccion1Exists(int id)
        {
            return _context.Piso1_Seccion1.Any(e => e.id == id);
        }
    }
}

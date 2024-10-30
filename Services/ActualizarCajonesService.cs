using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using UsuariosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace UsuariosApi.Services
{
    public class ActualizarCajonesService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ActualizarCajonesService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var currentTime = DateTime.Now;
                var nextMidnight = DateTime.Today.AddDays(1);

                // Calcular el tiempo hasta la próxima medianoche
                var delay = nextMidnight - currentTime;

                // Espera hasta la medianoche
                await Task.Delay(delay, stoppingToken);

                // Ejecuta la actualización de los cajones a "Disponible"
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<UsuarioContext>();

                    // Actualizar cajones en Piso1_Seccion1
                    var cajonesOcupadosSeccion1 = await context.Piso1_Seccion1
                        .Where(c => c.Estado == "Ocupado")
                        .ToListAsync(stoppingToken);

                    foreach (var cajon in cajonesOcupadosSeccion1)
                    {
                        cajon.Estado = "Disponible";
                        cajon.Nombre = null;
                        cajon.Email = null;
                    }

                    // Actualizar cajones en Piso1_Seccion2
                    var cajonesOcupadosSeccion2 = await context.Piso1_Seccion2
                        .Where(c => c.Estado == "Ocupado")
                        .ToListAsync(stoppingToken);

                    foreach (var cajon in cajonesOcupadosSeccion2)
                    {
                        cajon.Estado = "Disponible";
                        cajon.Nombre = null;
                        cajon.Email = null;
                    }

                    await context.SaveChangesAsync(stoppingToken);
                }
            }
        }
    }
}

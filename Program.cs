using UsuariosApi.Models;
using UsuariosApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Registrar servicios
builder.Services.AddControllers();
builder.Services.AddDbContext<UsuarioContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"))
);
builder.Services.AddHostedService<ActualizarCajonesService>(); // Aquí se registra el servicio de fondo

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplicar la política CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

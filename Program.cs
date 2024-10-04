using gymapiweb.Data;
using gymapiweb.Services; // Asegúrate de incluir el espacio de nombres para AuthService
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllers(); // Añadir controladores
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar la conexión a SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar el AuthService
builder.Services.AddScoped<AuthService>(); 

var app = builder.Build();

// Configurar el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseStaticFiles(); // Esto permite servir archivos estáticos desde wwwroot


// Mapear los controladores
app.MapControllers(); // Esto permite que tus controladores como ClientesController funcionen

app.Run();

using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using gymapiweb.Models;
using System.Threading.Tasks;

namespace gymapiweb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Definir la tabla Clientes como DbSet
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Empleado> Empleados { get; set; } // Asegúrate de agregar esta línea para Empleados

        // Método para iniciar sesión de Empleado
        public async Task<Empleado> LoginEmpleadoAsync(string correo, string password)
        {
            var empleado = await Empleados
                .FromSqlRaw("EXEC sp_LoginEmpleado @Correo, @Password", 
                            new SqlParameter("@Correo", correo), // Cambié de Usuario a Correo
                            new SqlParameter("@Password", password))
                .FirstOrDefaultAsync();

            return empleado;
        }

        // Método para iniciar sesión de Cliente
        public async Task<bool> LoginClienteAsync(string correoElectronico, string password)
        {
            // Ejecuta el procedimiento almacenado para iniciar sesión.
            var result = await Database.ExecuteSqlRawAsync(
                "EXEC sp_LoginCliente @CorreoElectronico, @Password", 
                new SqlParameter("@CorreoElectronico", correoElectronico), 
                new SqlParameter("@Password", password)
            );

            // Retorna true si se afectó al menos una fila, lo que indica un inicio de sesión exitoso.
            return result > 0;
        }
    }
}

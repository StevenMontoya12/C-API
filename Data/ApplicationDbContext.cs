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
            public async Task<Cliente> LoginClienteAsync(string correoElectronico, string password)
            {
                var cliente = await Clientes
                    .FromSqlRaw("EXEC sp_LoginCliente @CorreoElectronico, @Password", 
                                new SqlParameter("@CorreoElectronico", correoElectronico), 
                                new SqlParameter("@Password", password))
                    .FirstOrDefaultAsync();

                return cliente; // Retorna el cliente encontrado o null
            }

    }
}

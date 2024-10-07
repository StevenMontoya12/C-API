using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using gymapiweb.Data;
using gymapiweb.Models;
using System.Linq; // Asegúrate de incluir LINQ para usar FirstOrDefault

namespace gymapiweb.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método para iniciar sesión de empleados con manejo de excepciones
        public async Task<Empleado?> LoginEmpleadoAsync(string usuario, string password)
        {
            try
            {
                var empleado = _context.Empleados
                    .FromSqlRaw("EXEC sp_LoginEmpleado @Usuario, @Password", 
                                 new SqlParameter("@Usuario", usuario),
                                 new SqlParameter("@Password", password))
                    .AsEnumerable() // Convierte la consulta a IEnumerable
                    .FirstOrDefault(); // Usa la versión sincrónica de FirstOrDefault

                return empleado;
            }
            catch (Exception ex)
            {
                // Loguear el error o manejarlo de alguna forma
                Console.WriteLine($"Error al iniciar sesión como empleado: {ex.Message}");
                return null;
            }
        }

        // Método para iniciar sesión de clientes con manejo de excepciones
        public async Task<Cliente?> LoginClienteAsync(string correoElectronico, string password)
        {
            try
            {
                var cliente = _context.Clientes
                    .FromSqlRaw("EXEC sp_LoginCliente @CorreoElectronico, @Password", 
                                 new SqlParameter("@CorreoElectronico", correoElectronico),
                                 new SqlParameter("@Password", password))
                    .AsEnumerable() // Convierte la consulta a IEnumerable
                    .FirstOrDefault(); // Usa la versión sincrónica de FirstOrDefault

                return cliente;
            }
            catch (Exception ex)
            {
                // Loguear el error o manejarlo de alguna forma
                Console.WriteLine($"Error al iniciar sesión como cliente: {ex.Message}");
                return null;
            }
        }
    }
}

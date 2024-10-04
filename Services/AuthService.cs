using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using gymapiweb.Data;
using gymapiweb.Models;

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
        public async Task<Empleado?> LoginEmpleadoAsync(string correo, string password)
        {
            try
            {
                // Ejecutar el procedimiento almacenado para empleados
                var empleado = await _context.Empleados
                    .FromSqlRaw("EXEC sp_LoginEmpleado @Correo, @Password", 
                                 new SqlParameter("@Correo", correo), // Asegúrate de que el nombre coincide con el parámetro esperado en el SP
                                 new SqlParameter("@Password", password))
                    .FirstOrDefaultAsync(); // Utilizar asíncronamente

                return empleado; // Devuelve el empleado si se encuentra
            }
            catch (Exception ex)
            {
                // Captura y loguea cualquier error
                Console.WriteLine($"Error al iniciar sesión como empleado: {ex.Message}");
                return null; // En caso de error, devuelve null
            }
        }

        // Método para iniciar sesión de clientes con manejo de excepciones
        public async Task<Cliente?> LoginClienteAsync(string correoElectronico, string password)
        {
            try
            {
                // Ejecutar el procedimiento almacenado para clientes
                var cliente = await _context.Clientes
                    .FromSqlRaw("EXEC sp_LoginCliente @CorreoElectronico, @Password", 
                                 new SqlParameter("@CorreoElectronico", correoElectronico), // Asegúrate de que el nombre coincide con el parámetro esperado en el SP
                                 new SqlParameter("@Password", password))
                    .FirstOrDefaultAsync(); // Utilizar asíncronamente

                return cliente; // Devuelve el cliente si se encuentra
            }
            catch (Exception ex)
            {
                // Captura y loguea cualquier error
                Console.WriteLine($"Error al iniciar sesión como cliente: {ex.Message}");
                return null; // En caso de error, devuelve null
            }
        }
    }
}
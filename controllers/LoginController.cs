using gymapiweb.Data; // Espacio de nombres correcto para el contexto de base de datos
using gymapiweb.Models; // Modelos necesarios
using gymapiweb.Services; // Servicios como AuthService
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Para utilizar el sistema de logging
using System.Threading.Tasks;

namespace gymapiweb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(AuthService authService, ILogger<LoginController> logger)
        {
            _authService = authService;
            _logger = logger; // Inyección del servicio de logging
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Verificar si los campos requeridos están presentes
            if (string.IsNullOrEmpty(request.CorreoElectronico) || string.IsNullOrEmpty(request.Password))
            {
                _logger.LogWarning("Faltan el correo electrónico o la contraseña en la solicitud");
                return BadRequest(new { message = "Correo electrónico y contraseña son requeridos" });
            }

            // Loguear el intento de inicio de sesión con los valores proporcionados
            _logger.LogInformation("Intentando iniciar sesión con el correo: {Correo}, y contraseña: {Password}", request.CorreoElectronico, request.Password);

            try
            {
                // Intenta iniciar sesión como empleado
                var empleado = await _authService.LoginEmpleadoAsync(request.CorreoElectronico, request.Password);
                if (empleado != null)
                {
                    _logger.LogInformation("Inicio de sesión exitoso para empleado con el correo: {Correo}", request.CorreoElectronico);
                    return Ok(new { Role = "Empleado", RedirectUrl = "/vista/empleado.html", User = empleado });
                }

                // Intenta iniciar sesión como cliente
                var cliente = await _authService.LoginClienteAsync(request.CorreoElectronico, request.Password);
                if (cliente != null)
                {
                    _logger.LogInformation("Inicio de sesión exitoso para cliente con el correo: {Correo}", request.CorreoElectronico);
                    return Ok(new { Role = "Cliente", RedirectUrl = "/vista/cliente.html", User = cliente });
                }

                // Si ambas autenticaciones fallan
                _logger.LogError("Inicio de sesión fallido para todas las cuentas con el correo: {Correo}", request.CorreoElectronico);
                return Unauthorized(new { message = "Credenciales inválidas" });
            }
            catch (Exception ex)
            {
                // Capturar cualquier error inesperado durante la autenticación
                _logger.LogError(ex, "Error inesperado durante el proceso de inicio de sesión para el correo: {Correo}", request.CorreoElectronico);
                return StatusCode(500, new { message = "Ocurrió un error en el servidor. Por favor, inténtelo de nuevo más tarde." });
            }
        }
    }

    // Clase para manejar la solicitud de inicio de sesión
    public class LoginRequest
    {
        public string? CorreoElectronico { get; set; } // Permitir nulos para flexibilidad
        public string? Password { get; set; } // Permitir nulos para flexibilidad
    }
}

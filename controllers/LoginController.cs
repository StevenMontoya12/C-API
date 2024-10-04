using gymapiweb.Data; // Asegúrate de que el espacio de nombres es correcto
using gymapiweb.Models; // Asegúrate de que este espacio de nombres esté presente
using gymapiweb.Services; // Asegúrate de que este espacio de nombres esté presente
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Asegúrate de incluir esto
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
            _logger = logger; // Inyectar el servicio de logging
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Log de intento de inicio de sesión
            _logger.LogInformation("Intentando iniciar sesión con el correo: {Correo}", request.CorreoElectronico);

            // Intenta iniciar sesión como empleado
            var empleado = await _authService.LoginEmpleadoAsync(request.CorreoElectronico, request.Password);
            if (empleado != null)
            {
                _logger.LogInformation("Inicio de sesión exitoso para empleado con el correo: {Correo}", request.CorreoElectronico);
                // Aquí rediriges a la vista de empleados
                return Ok(new { Role = "Empleado", RedirectUrl = "/vista/empleado", User = empleado });
            }

            // Intenta iniciar sesión como cliente
            var cliente = await _authService.LoginClienteAsync(request.CorreoElectronico, request.Password);
            if (cliente != null)
            {
                _logger.LogInformation("Inicio de sesión exitoso para cliente con el correo: {Correo}", request.CorreoElectronico);
                // Aquí rediriges a la vista de clientes
                return Ok(new { Role = "Cliente", RedirectUrl = "/vista/cliente", User = cliente });
            }

            // Log cuando todas las credenciales son inválidas
            _logger.LogError("Inicio de sesión fallido para todas las cuentas con el correo: {Correo}", request.CorreoElectronico);
            return Unauthorized(new { message = "Credenciales inválidas", correo = request.CorreoElectronico });
        }
    }

    public class LoginRequest
    {
        public string? CorreoElectronico { get; set; } // Permitir valores nulos
        public string? Password { get; set; } // Permitir valores nulos
    }
}

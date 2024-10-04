using Microsoft.AspNetCore.Mvc;
using gymapiweb.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace gymapiweb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmpleadoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/empleado/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpleadoById(Guid id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound(new { message = "Empleado no encontrado" });
            }

            return Ok(empleado); // Devolver los datos del empleado en formato JSON
        }
    }
}

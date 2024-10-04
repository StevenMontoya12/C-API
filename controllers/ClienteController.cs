using Microsoft.AspNetCore.Mvc;
using gymapiweb.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace gymapiweb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/cliente/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClienteById(Guid id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound(new { message = "Cliente no encontrado" });
            }

            return Ok(cliente); // Devolver los datos del cliente en formato JSON
        }
    }
}

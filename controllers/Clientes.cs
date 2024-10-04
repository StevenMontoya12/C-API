using gymapiweb.Data;
using gymapiweb.Models;  
using Microsoft.AspNetCore.Mvc;
using System.Linq;  

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ClientesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/clientes
    [HttpGet]
    public IActionResult GetClientes()
    {
        var clientes = _context.Clientes.ToList();  // Obtiene todos los clientes de la base de datos
        
        if (clientes == null || !clientes.Any())
        {
            return NotFound("No se encontraron clientes.");  // Devuelve un 404 si no hay clientes
        }

        return Ok(clientes);  // Devuelve los clientes en formato JSON
    }
}

using Microsoft.AspNetCore.Mvc;
using BarberAkji.API.Data; // For at bruge DapperContext
using BarberAkji.Models.Entities; // For at bruge Service-klassen
using Dapper;

namespace BarberAkji.API.Controllers
{
    [ApiController] // Angiver at denne klasse er en API-controller
    [Route("api/[controller]")] // Ruten bliver /api/service (controller-navn i småt)
    public class ServiceController : ControllerBase
    {
        private readonly DapperContext _context; // Kontekst til SQL-forbindelse

        public ServiceController(DapperContext context) // Dependency Injection af DapperContext
        {
            _context = context;
        }

        [HttpGet] // GET-endpoint til at hente alle services/ydelser
        public async Task<ActionResult<IEnumerable<Service>>> GetAllServices()
        {
            using var connection = _context.CreateConnection(); // Åbner SQL-forbindelse
            var services = await connection.QueryAsync<Service>("SELECT * FROM Services"); // Henter alle ydelser fra databasen
            return Ok(services); // Returnerer listen som JSON
        }
    }
}

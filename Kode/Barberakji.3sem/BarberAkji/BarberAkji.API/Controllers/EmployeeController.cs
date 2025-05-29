using Microsoft.AspNetCore.Mvc;
using BarberAkji.API.Data; // For at bruge DapperContext
using BarberAkji.Models.Entities; // For at bruge Employee-klassen
using Dapper;

namespace BarberAkji.API.Controllers
{
    [ApiController] // Angiver at denne klasse er en API-controller
    [Route("api/[controller]")] // Ruten bliver /api/employee (controller-navn i småt)
    public class EmployeeController : ControllerBase
    {
        private readonly DapperContext _context; // Kontekst til SQL-forbindelse

        public EmployeeController(DapperContext context) // Dependency Injection af DapperContext
        {
            _context = context;
        }

        [HttpGet] // GET-endpoint til at hente alle ansatte/barberer
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            using var connection = _context.CreateConnection(); // Åbner SQL-forbindelse
            var employees = await connection.QueryAsync<Employee>("SELECT * FROM Employees"); // Henter alle ansatte fra databasen
            return Ok(employees); // Returnerer listen som JSON
        }
    }
}

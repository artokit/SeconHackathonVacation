using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;

namespace Company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            // Временная заглушка
            var company = new CompanyModel();

            return Ok(company);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            // Временная заглушка
            var company = new CompanyModel();

            if (company == null)
                return NotFound(new { Message = "Company not found" });

            return Ok(company);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CompanyModel company)
        {
            if (company == null || string.IsNullOrEmpty(company.Name))
                return BadRequest(new { Message = "Invalid company data" });

            company.Id = Guid.NewGuid();
            // Будет метод на сервис по добавлению компании в бд
            return CreatedAtAction(nameof(GetById), new { id = company.Id }, company);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] CompanyModel updatedCompany)
        {
            // Временная заглушка
            var company = new CompanyModel();

            if (company == null)
                return NotFound(new { Message = "Company not found" });

            if (updatedCompany == null || string.IsNullOrEmpty(updatedCompany.Name))
                return BadRequest(new { Message = "Invalid company data" });

            company.Name = updatedCompany.Name;
            company.Description = updatedCompany.Description;
            company.Supervisor_Id = updatedCompany.Supervisor_Id;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            // Временная заглушка
            var company = new CompanyModel();

            if (company == null)
                return NotFound(new { Message = "Company not found" });

            // Будет вызов Remove в сервисе
            return NoContent();
        }
    }
}
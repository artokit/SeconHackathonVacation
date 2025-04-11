using Api.Dto.Schemas.Requests;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("vacation-service/schemas")]
    [Authorize(Roles = "Hr,Director")]
    public class SchemasController : ControllerBase
    {
        private readonly SchemasService _schemasService;

        public SchemasController(SchemasService schemasService)
        {
            _schemasService = schemasService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSchemaRequestDto dto)
        {
            var result = await _schemasService.AddAsync(dto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _schemasService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSchemaRequestDto dto)
        {
            var result = await _schemasService.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _schemasService.DeleteAsync(id);
            return NoContent();
        }
    }
}

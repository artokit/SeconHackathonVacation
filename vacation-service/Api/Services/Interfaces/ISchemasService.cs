using DataAccess.Models;
using Api.Dto.Schemas.Requests;
using Api.Dto.Schemas.Responses;

namespace Api.Services.Interfaces
{
    public interface ISchemasService
    {
        public Task<GetSchemaResponseDto> AddAsync(CreateSchemaRequestDto schema);
        public Task<GetSchemaResponseDto?> GetByIdAsync(Guid id);
        public Task<GetSchemaResponseDto> UpdateAsync(Guid id, UpdateSchemaRequestDto dto);
        public Task DeleteAsync(Guid id);
    }
}

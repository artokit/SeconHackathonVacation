using Api.Services.Interfaces;
using DataAccess.Common.Interfaces.Repositories;
using Api.Dto.Schemas.Requests;
using Api.Dto.Schemas.Responses;
using Api.Exceptions.Schemas;
using Api.Mappers;

namespace Api.Services
{
    public class SchemasService : ISchemasService
    {
        private readonly ISchemasRepository _schemasRepository;

        public SchemasService(ISchemasRepository schemasRepository)
        { 
            _schemasRepository = schemasRepository;
        }

        public async Task<GetSchemaResponseDto> AddAsync(CreateSchemaRequestDto dto)
        {
            var schema = dto.MapToDb();
            var result = await _schemasRepository.AddAsync(schema);
            return result.MapToDto();
        }

        public async Task<GetSchemaResponseDto> GetByIdAsync(Guid id)
        {
            var schema = await _schemasRepository.GetByIdAsync(id);

            if (schema is null)
                throw new SchemaNotFoundException(); 

            return schema.MapToDto();
        }

        public async Task<GetSchemaResponseDto> UpdateAsync(Guid id, UpdateSchemaRequestDto dto)
        {
            var oldSchema = await _schemasRepository.GetByIdAsync(id);

            if (oldSchema is null)
                throw new SchemaNotFoundException(); 

            var dbSchema = dto.MapToDb(oldSchema);
            var updated = await _schemasRepository.UpdateAsync(dbSchema);
            return updated.MapToDto();
        }

        public async Task DeleteAsync(Guid id)
        {
            var schema = await _schemasRepository.GetByIdAsync(id);

            if (schema is null)
                throw new SchemaNotFoundException();

            await _schemasRepository.DeleteAsync(id);
        }
    }
}

using Api.Dto.Schemas.Requests;
using Api.Dto.Schemas.Responses;
using DataAccess.Models;

namespace Api.Mappers
{
    public static class SchemaMapper
    {
        public static DbSchema MapToDb(this CreateSchemaRequestDto requestDto)
        {
            return new DbSchema
            {
                Id = Guid.NewGuid(),
                UserId = requestDto.UserId,
                Name = requestDto.Name
            };
        }

        public static DbSchema MapToDb(this UpdateSchemaRequestDto requestDto, DbSchema oldSchema)
        {
            return new DbSchema
            {
                Id = oldSchema.Id,
                // UserId = existing.UserId, айди создателя не редактируется?
                Name = requestDto.Name ?? oldSchema.Name
            };
        }

        public static GetSchemaResponseDto MapToDto(this DbSchema dbSchema)
        {
            return new GetSchemaResponseDto
            {
                Id = dbSchema.Id,
                UserId = dbSchema.UserId,
                Name = dbSchema.Name
            };
        }

        public static List<GetSchemaResponseDto> MapToDto(this List<DbSchema?> dbSchemas)
        {
            return dbSchemas
                .Where(s => s != null)
                .Select(s => s!.MapToDto())
                .ToList();
        }


    }
}

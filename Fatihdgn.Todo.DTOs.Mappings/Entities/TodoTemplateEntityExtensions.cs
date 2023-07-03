using Fatihdgn.Todo.Entities;
using System.Text.Json;

namespace Fatihdgn.Todo.DTOs.Mappings.Entities
{
    public static class TodoTemplateEntityExtensions
    {
        private static JsonDocument AsJsonDocument<T>(T value) => JsonDocument.Parse(JsonSerializer.SerializeToUtf8Bytes(value));

        public static TodoTemplateEntity ToEntity(this TodoTemplateDTO entity) => new TodoTemplateEntity
        {
            Id = entity.Id,
            Name = entity.Name,
            Contents = entity.Contents
        };

        public static TodoTemplateDTO ToDTO(this TodoTemplateEntity entity) => new TodoTemplateDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            Contents = entity.Contents ?? new List<string>()
        };

        public static TodoTemplateCreateDTO ToCreateDTO(this TodoTemplateEntity entity) => new TodoTemplateCreateDTO
        {
            Name = entity.Name,
            Contents = entity.Contents ?? new List<string>()
        };

        public static TodoTemplateUpdateDTO ToUpdateDTO(this TodoTemplateEntity entity) => new TodoTemplateUpdateDTO
        {
            Name = entity.Name,
            Contents = entity.Contents ?? new List<string>()
        };

        public static TodoTemplatePatchDTO ToPatchDTO(this TodoTemplateEntity entity) => new TodoTemplatePatchDTO
        {
            Name = entity.Name,
            Contents = entity.Contents ?? null
        };

        public static TodoTemplateEntity ApplyTo(this TodoTemplateCreateDTO self, TodoTemplateEntity entity)
        {
            entity.Name = self.Name;
            entity.Contents = self.Contents;
            return entity;
        }

        public static TodoTemplateEntity ApplyTo(this TodoTemplateUpdateDTO self, TodoTemplateEntity entity)
        {
            entity.Name = self.Name;
            entity.Contents = self.Contents;
            return entity;
        }

        public static TodoTemplateEntity ApplyTo(this TodoTemplatePatchDTO self, TodoTemplateEntity entity)
        {
            entity.Name = self.Name ?? entity.Name;
            if (self.Contents is not null)
                entity.Contents = self.Contents;
            return entity;
        }
    }
}

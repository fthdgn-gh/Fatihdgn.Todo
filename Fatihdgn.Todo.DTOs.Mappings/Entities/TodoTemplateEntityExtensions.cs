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
            Content = AsJsonDocument(entity.Content)
        };

        public static TodoTemplateDTO ToDTO(this TodoTemplateEntity entity) => new TodoTemplateDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            Content = entity.Content?.Deserialize<List<string>>() ?? new List<string>()
        };

        public static TodoTemplateCreateDTO ToCreateDTO(this TodoTemplateEntity entity) => new TodoTemplateCreateDTO
        {
            Name = entity.Name,
            Content = entity.Content?.Deserialize<List<string>>() ?? new List<string>()
        };

        public static TodoTemplateUpdateDTO ToUpdateDTO(this TodoTemplateEntity entity) => new TodoTemplateUpdateDTO
        {
            Name = entity.Name,
            Content = entity.Content?.Deserialize<List<string>>() ?? new List<string>()
        };

        public static TodoTemplatePatchDTO ToPatchDTO(this TodoTemplateEntity entity) => new TodoTemplatePatchDTO
        {
            Name = entity.Name,
            Content = entity.Content?.Deserialize<List<string>>() ?? null
        };

        public static TodoTemplateEntity ApplyTo(this TodoTemplateCreateDTO self, TodoTemplateEntity entity)
        {
            entity.Name = self.Name;
            entity.Content = AsJsonDocument(self.Content);
            return entity;
        }

        public static TodoTemplateEntity ApplyTo(this TodoTemplateUpdateDTO self, TodoTemplateEntity entity)
        {
            entity.Name = self.Name;
            entity.Content = AsJsonDocument(self.Content);
            return entity;
        }

        public static TodoTemplateEntity ApplyTo(this TodoTemplatePatchDTO self, TodoTemplateEntity entity)
        {
            entity.Name = self.Name ?? entity.Name;
            if (self.Content is not null)
                entity.Content = AsJsonDocument(self.Content);
            return entity;
        }
    }
}

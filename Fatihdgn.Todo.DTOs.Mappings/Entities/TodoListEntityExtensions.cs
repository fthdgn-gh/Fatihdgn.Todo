using Fatihdgn.Todo.Entities;

namespace Fatihdgn.Todo.DTOs.Mappings.Entities
{
    public static class TodoListEntityExtensions
    {
        public static TodoListEntity ToEntity(this TodoListDTO entity) => new TodoListEntity
        {
            Id = entity.Id,
            Name = entity.Name,
        };

        public static TodoListDTO ToDTO(this TodoListEntity entity) => new TodoListDTO
        {
            Id = entity.Id,
            Name = entity.Name,
        };

        public static TodoListCreateDTO ToCreateDTO(this TodoListEntity entity) => new TodoListCreateDTO
        {
            Name = entity.Name,
        };

        public static TodoListUpdateDTO ToUpdateDTO(this TodoListEntity entity) => new TodoListUpdateDTO
        {
            Name = entity.Name,
        };

        public static TodoListPatchDTO ToPatchDTO(this TodoListEntity entity) => new TodoListPatchDTO
        {
            Name = entity.Name,
        };

        public static TodoListEntity ApplyTo(this TodoListCreateDTO self, TodoListEntity entity)
        {
            entity.Name = self.Name;
            return entity;
        }

        public static TodoListEntity ApplyTo(this TodoListUpdateDTO self, TodoListEntity entity)
        {
            entity.Name = self.Name;
            return entity;
        }

        public static TodoListEntity ApplyTo(this TodoListPatchDTO self, TodoListEntity entity)
        {
            entity.Name = self.Name ?? entity.Name;
            return entity;
        }
    }
}

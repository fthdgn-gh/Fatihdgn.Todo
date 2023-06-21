using Fatihdgn.ToDo.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fatihdgn.Todo.Context;

public class ToDoContext : DbContext
{
    public ToDoContext(DbContextOptions options) : base(options) { }
    
    public DbSet<TodoItemEntity> Items { get; set; }
}
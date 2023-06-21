using Fatihdgn.Todo.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fatihdgn.Todo.Context;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions options) : base(options) { }
    
    public DbSet<TodoItemEntity> Items { get; set; }
}
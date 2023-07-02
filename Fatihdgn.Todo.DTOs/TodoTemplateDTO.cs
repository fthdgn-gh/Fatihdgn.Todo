namespace Fatihdgn.Todo.DTOs;

public class TodoTemplateDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<string> Content { get; set; } = new List<string>();
}

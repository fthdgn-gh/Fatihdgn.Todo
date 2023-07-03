namespace Fatihdgn.Todo.DTOs;

public class TodoTemplateCreateDTO
{
    public string Name { get; set; } = string.Empty;
    public List<string> Contents { get; set; } = new List<string>();
}

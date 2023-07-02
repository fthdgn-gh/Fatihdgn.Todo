namespace Fatihdgn.Todo.DTOs;

public class TodoTemplateUpdateDTO
{
    public string Name { get; set; } = string.Empty;
    public List<string> Content { get; set; } = new List<string>();
}

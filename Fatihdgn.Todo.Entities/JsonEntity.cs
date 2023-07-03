using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fatihdgn.Todo.Entities;

public class JsonEntity<T>
{
    private static T? @default = default(T);
    private string? serialized;
    private T? value = default;
    private bool isChanged = false;

    [JsonIgnore]
    public string? Serialized
    {
        get => serialized;
        set
        {
            if (serialized != value)
            {
                isChanged = true;
                serialized = value;
            }

        }
    }

    [NotMapped]
    public T? Value
    {
        get
        {
            if (isChanged || @default?.Equals(value) != true)
            {
                value = serialized == null ? default : JsonSerializer.Deserialize<T>(serialized);
                isChanged = false;
            }
            return value;
        }
        set
        {
            this.value = value;
            serialized = JsonSerializer.Serialize(value);
        }
    }

    public static implicit operator T?(JsonEntity<T>? entity) => entity == null ? default : entity.Value;
    public static implicit operator JsonEntity<T>(T? value) => new() { Value = value };

    public static implicit operator string?(JsonEntity<T>? entity) => entity == null ? default : entity.Serialized;
    public static implicit operator JsonEntity<T>(string? serialized) => new() { Serialized = serialized };
}

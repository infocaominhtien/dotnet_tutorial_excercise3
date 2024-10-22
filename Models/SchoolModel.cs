using System.Text.Json;

namespace HttpClientProject.Models;

public class SchoolModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? Address { get; set; }

    public static SchoolModel FromJson(string json) => JsonSerializer.Deserialize<SchoolModel>(json,
        new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException();
}
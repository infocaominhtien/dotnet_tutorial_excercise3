using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace HttpClientProject;

public class HttpClientHelper
{
    public static async Task<T?> GetAsync<T>(string url, string? token = null, string? correlationId = null)
    {
        using HttpClient client = new HttpClient();

        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        if (!string.IsNullOrEmpty(correlationId))
        {
            client.DefaultRequestHeaders.Add("x-correlation-id", correlationId);
        }

        var response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        throw new HttpRequestException(response.ReasonPhrase);
    }
}
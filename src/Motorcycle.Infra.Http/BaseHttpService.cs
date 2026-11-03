using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Text;

namespace Motorcycle.Infra.Http;

public abstract class BaseHttpService
{
    private readonly HttpClient _httpClient;

    protected BaseHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    protected async Task<T> GetAsync<T>(string url, NameValueCollection? queryParams) where T : new()
    {
        if (queryParams is not null)
        {
            url = $"{url}?{queryParams}";
        }

        var response = await _httpClient.GetAsync(url);

        response.EnsureSuccessStatusCode();

        return await DeserializeResponse<T>(response);
    }

    protected async Task<T> GetAsync<T>(string url) where T : new()
    {
        return await GetAsync<T>(url, null);
    }

    protected async Task<T> PostAsync<T>(string url, object request) where T : new()
    {
        var json = JsonConvert.SerializeObject(request);
        var body = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, body);

        response.EnsureSuccessStatusCode();

        return await DeserializeResponse<T>(response);
    }

    private static async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
    {
        var responseMessage = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseMessage)!;
    }
}


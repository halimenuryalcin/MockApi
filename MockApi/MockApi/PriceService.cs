using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

public class PriceService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public PriceService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["GoldApi:ApiKey"];

        // API key'i x-access-token başlığına ekliyoruz
        _httpClient.DefaultRequestHeaders.Add("x-access-token", _apiKey);
    }

    public async Task<double> GetGoldPricePerGramAsync()
    {
        var url = "https://www.goldapi.io/api/XAU/USD";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"API çağrısı başarısız: {(int)response.StatusCode} {response.ReasonPhrase}");
        }

        using var stream = await response.Content.ReadAsStreamAsync();
        using var json = await JsonDocument.ParseAsync(stream);

        if (json.RootElement.TryGetProperty("price", out var priceElement))
        {
            double pricePerOunce = priceElement.GetDouble();
            return pricePerOunce / 31.1035; // 1 ons ≈ 31.1035 gram
        }

        throw new Exception("Altın fiyatı JSON içinde bulunamadı.");
    }
}

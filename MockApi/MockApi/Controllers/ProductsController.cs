using Microsoft.AspNetCore.Mvc;
using MockApi;
using MockApi.Models;
using Newtonsoft.Json;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly PriceService _goldPriceService;

    public ProductsController(PriceService goldPriceService)
    {
        _goldPriceService = goldPriceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var json = await System.IO.File.ReadAllTextAsync("C:\\Users\\nurre\\OneDrive\\Desktop\\FullStackCase\\MockApi\\MockApi\\Data\\ProductData.json");
        var products = JsonConvert.DeserializeObject<List<Product>>(json);

        double goldPrice = await _goldPriceService.GetGoldPricePerGramAsync();

        var result = products.Select(p => new ProductResponse
        {
            name = p.name,
            weight = p.weight,
            Images = p.Images, // Dictionary<string, string> ile birebir uyumlu
            priceUSD = Math.Round((p.popularityScore + 1) * p.weight * goldPrice, 2)
        });


        return Ok(result);
    }
}

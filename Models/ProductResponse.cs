namespace MockApi.Models
{
    public class ProductResponse
    {
        public string name { get; set; }
        public double priceUSD { get; set; }
        public double weight { get; set; }
        public Dictionary<string, string> Images { get; set; }
    }
}

namespace MockApi.Models
{
    public class Product
    {
        public string name { get; set; }
        public double popularityScore { get; set; }
        public double weight { get; set; }
        public Dictionary<string, string> Images { get; set; }
    }
}

namespace CampTravelGear.DTOs
{
    public class CountryApiResponse
    {
        public bool Error { get; set; }
        public string Msg { get; set; } = string.Empty;
        public List<CountryData> Data { get; set; } = new();
    }

    public class CountryData
    {
        public string Country { get; set; } = string.Empty;
        public List<string> Cities { get; set; } = new();
    }
}

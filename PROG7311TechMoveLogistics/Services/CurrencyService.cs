using System.Text.Json;

namespace PROG7311TechMoveLogistics.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CurrencyService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _configuration = config;
        }

        public async Task<double> GetToZarRateAsync()
        {
            try
            {
                // api key is stored in app settings.json
                var url = _configuration["ExchangeRate:ApiUrl"];

                if (string.IsNullOrEmpty(url))
                    throw new Exception("API URL is not configured.");

                var response = await _httpClient.GetStringAsync(url);

                var json = JsonSerializer.Deserialize<JsonElement>(response);

                var rate = json
                    .GetProperty("conversion_rates")
                    .GetProperty("ZAR")
                    .GetDouble();

                return rate;
            }
            catch (HttpRequestException)
            {
                throw new Exception("Unable to connect to the currency API. Please try again later.");
            }
            catch (JsonException)
            {
                throw new Exception("Error processing currency data from API.");
            }
            catch (Exception)
            {
                throw new Exception("An unexpected error occurred while retrieving exchange rate.");
            }

        }
    }
}
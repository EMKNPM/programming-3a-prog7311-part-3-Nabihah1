using System.Text.Json;

namespace TechMoveLogisticsAPI.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CurrencyService(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<double> GetToZarRateAsync()
        {
            try
            {
                var url = _configuration["ExchangeRate:ApiUrl"];

                if (string.IsNullOrEmpty(url))
                {
                    throw new Exception(
                        "API URL is not configured.");
                }

                var response =
                    await _httpClient.GetStringAsync(url);

                var json =
                    JsonSerializer.Deserialize<JsonElement>(
                        response);

                var rate = json
                    .GetProperty("conversion_rates")
                    .GetProperty("ZAR")
                    .GetDouble();

                return rate;
            }
            catch (HttpRequestException)
            {
                throw new Exception(
                    "Unable to connect to the currency API.");
            }
            catch (JsonException)
            {
                throw new Exception(
                    "Error processing currency data.");
            }
            catch (Exception)
            {
                throw new Exception(
                    "Unexpected error retrieving exchange rate.");
            }
    }
    }
}

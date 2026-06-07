namespace TechMoveLogisticsAPI.Services
{
    public interface ICurrencyService
    {
        //saves the amount, to show on index page
        Task<double> GetToZarRateAsync();
    }
}

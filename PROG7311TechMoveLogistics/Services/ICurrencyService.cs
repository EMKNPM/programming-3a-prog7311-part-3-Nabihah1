namespace PROG7311TechMoveLogistics.Services
{
    public interface ICurrencyService
    {
        
        //saves the amount, to show on index page
        Task<double> GetToZarRateAsync();

    }
}

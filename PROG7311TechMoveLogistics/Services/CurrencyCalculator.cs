

namespace PROG7311TechMoveLogistics.Services
{
    public class CurrencyCalculator
    {

        //this class has nothing to do with the code 
        //its just for testing purporses 
        // it vefies that the USD is converted to ZAR propelry 
        // so that we can run proper unit tests later on 

        public double CalculateZar(double usdAmount, double rate)
        {
            if (usdAmount < 0)
                throw new ArgumentException("USD amount cannot be negative.");

            if (rate <= 0)
                throw new ArgumentException("Rate must be greater than zero.");

            return usdAmount * rate;
        }
    }
}

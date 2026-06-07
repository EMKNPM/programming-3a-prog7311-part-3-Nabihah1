using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using PROG7311TechMoveLogistics.Data;
using PROG7311TechMoveLogistics.Models;
using PROG7311TechMoveLogistics.Services;
using Xunit;

namespace PROG7311TechMoveLogistics.Tests
{


    // Fake service used to avoid calling the real currency API during testing
    public class FakeCurrencyService : ICurrencyService
    {
        public Task<double> GetToZarRateAsync()
        {
            return Task.FromResult(18.50);
        }
    }

    public class TechMoveTests
    {


        //ensures USD TO ZAR works properly 
        [Fact]
        public void Test1_CalculateZar_Successful()
        {
            var calculator = new CurrencyCalculator();

            double usdAmount = 100;
            double rate = 18.50;

            var result = calculator.CalculateZar(usdAmount, rate);

            Assert.Equal(1850, result);
        }


        // tests edge cases (i.e. where USD = 0) 
        [Fact]
        public void Test2_CalculateZar_ZeroAmount()
        {
            var calculator = new CurrencyCalculator();

            double usdAmount = 0;
            double rate = 18.50;

            var result = calculator.CalculateZar(usdAmount, rate);

            Assert.Equal(0, result);
        }


        //tests invalid rate (e.g. the rate is 0)
        [Fact]
        public void Test3_CalculateZar_Failure()
        {
            var calculator = new CurrencyCalculator();

            Assert.Throws<ArgumentException>(() => calculator.CalculateZar(100, 0));
        }

        //tests a .pdf file 
        [Fact]
        public void Test4_ValidateFile_Successful()
        {
            var fileValidator = new FileValidations();

            var exception = Record.Exception(() => fileValidator.ValidatePdfFile("agreement.pdf"));

            Assert.Null(exception);
        }

        //tests .exe file types
        [Fact]
        public void Test5_ValidateFile_Unsessussful()
        {
            var fileValidator = new FileValidations();

            Assert.Throws<InvalidOperationException>(() => fileValidator.ValidatePdfFile("fail.exe"));
        }


        //tests what happens when u upload an empty file
        [Fact]
        public void Test6_ValidateEmptyFileName()
        {
            var fileValidator = new FileValidations();

            Assert.Throws<ArgumentException>(() => fileValidator.ValidatePdfFile(""));
        }

        //trying to request a service, with contract = expired
        // should not be able to do it
        //[Fact]
        //public async Task Test7_CreateServiceRequest_ExpiredContract()
        //{
        //    var options = new DbContextOptionsBuilder<DataContext>()
        //        .UseInMemoryDatabase(databaseName: "ExpiredContractDb")
        //        .Options;

        //    using var context = new DataContext(options);

        //    var contract = new Contract
        //    {
        //        ContractId = 1,
        //        ContractStartDate = DateTime.Now.AddDays(-10),
        //        ContractEndDate = DateTime.Now.AddDays(10),
        //        ContractStatus = ContractStatus.Expired,
        //        ContractServiceLevel = "Standard",
        //        ClientId = 1
        //    };

        //    var currencyService = new FakeCurrencyService();
        //    var service = new ServiceRequestService(context, currencyService);

        //    var request = new ServiceRequest
        //    {
        //        ServiceRequestId = 1,
        //        SrDescription = "Test request",
        //        CostForeign = 100,
        //        SrStatus = ServiceRequestStatus.Pending,
        //        ContractId = 1
        //    };

        //    await Assert.ThrowsAsync<Exception>(() => service.CreateServiceRequest(request));
        //}


        //trying to request a service, with contract = on hold
        // should not be able to do it
        //[Fact]
        //public async Task Test8_CreateServiceRequest_OnHoldContract()
        //{
        //    var options = new DbContextOptionsBuilder<DataContext>()
        //       .UseInMemoryDatabase(databaseName: "OnHoldContractDb")
        //       .Options;

        //    using var context = new DataContext(options);

        //    var contract = new Contract
        //    {
        //        ContractId = 2,
        //        ContractStartDate = DateTime.Now.AddDays(-10),
        //        ContractEndDate = DateTime.Now.AddDays(10),
        //        ContractStatus = ContractStatus.OnHold,
        //        ContractServiceLevel = "Premium",
        //        ClientId = 1
        //    };

        //    context.Contracts.Add(contract);
        //    await context.SaveChangesAsync();

        //    var currencyService = new FakeCurrencyService();
        //    var service = new ServiceRequestService(context, currencyService);

        //    var request = new ServiceRequest
        //    {
        //        ServiceRequestId = 2,
        //        SrDescription = "Blocked request",
        //        CostForeign = 200,
        //        SrStatus = ServiceRequestStatus.Pending,
        //        ContractId = 2
        //    };

        //    await Assert.ThrowsAsync<Exception>(() => service.CreateServiceRequest(request));
        //}


        // tests that u can usccessfully request a service (contract status = active)
        //[Fact]
        //public async Task Test9_CreateServiceRequest_ActiveContract()
        //{

        //    var options = new DbContextOptionsBuilder<DataContext>()
        //        .UseInMemoryDatabase(databaseName: "ActiveContractDb")
        //        .Options;

        //    using var context = new DataContext(options);

        //    var contract = new Contract
        //    {
        //        ContractId = 3,
        //        ContractStartDate = DateTime.Now.AddDays(-10),
        //        ContractEndDate = DateTime.Now.AddDays(10),
        //        ContractStatus = ContractStatus.Active,
        //        ContractServiceLevel = "Premium",
        //        ClientId = 1
        //    };

        //    context.Contracts.Add(contract);
        //    await context.SaveChangesAsync();

        //    var currencyService = new FakeCurrencyService();
        //    var service = new ServiceRequestService(context, currencyService);

        //    var request = new ServiceRequest
        //    {
        //        ServiceRequestId = 3,
        //        SrDescription = "Allowed request",
        //        CostForeign = 100,
        //        SrStatus = ServiceRequestStatus.Pending,
        //        ContractId = 3
        //    };

        //    await service.CreateServiceRequest(request);

        //    var savedRequest = await context.ServiceRequests
        //        .FirstOrDefaultAsync(x => x.ServiceRequestId == 3);

        //    Assert.NotNull(savedRequest);
        //    Assert.Equal(1850, savedRequest.CostZAR);
        //}
    }
}
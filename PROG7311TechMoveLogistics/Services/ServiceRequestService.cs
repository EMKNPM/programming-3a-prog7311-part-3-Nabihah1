using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using PROG7311TechMoveLogistics.Data;
using PROG7311TechMoveLogistics.Models; 

namespace PROG7311TechMoveLogistics.Services
{
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly DataContext _context;
        private readonly ICurrencyService _currencyService;

        public ServiceRequestService(DataContext context, ICurrencyService currencyService)
        {
            _context = context;
            _currencyService = currencyService;
        }

        public async Task CreateServiceRequest(ServiceRequest serviceRequest)
        {

            var contracts = await _context.Contracts.FirstOrDefaultAsync(
                    c => c.ContractId == serviceRequest.ContractId);

            if (contracts == null)
            {
                throw new Exception("Contract not found");
            }

            //implement state design pattern - for accepting a service 
            if (contracts.ContractStatus == ContractStatus.Expired ||
                contracts.ContractStatus == ContractStatus.OnHold)
            {
                throw new Exception("Cannot create a service request for Expired or On Hold contracts");
            }


            // making all values in appear in RANDs using apis 

            // get exchange rate from currecyService
            var rate = await _currencyService.GetToZarRateAsync();
            //calculate rand cost 
            serviceRequest.CostZAR = serviceRequest.CostForeign * rate;


            //save 
            _context.ServiceRequests.Add(serviceRequest);
            await _context.SaveChangesAsync();


        }


        public async Task EditServiceRequest(ServiceRequest updatedRequest)
        {
            var existingRequest = await _context.ServiceRequests
                .FirstOrDefaultAsync(r => r.ServiceRequestId == updatedRequest.ServiceRequestId);

            if (existingRequest == null)
            {
                throw new Exception("Service request not found.");
            }

            var contract = await _context.Contracts
                .FirstOrDefaultAsync(c => c.ContractId == updatedRequest.ContractId);

            if (contract == null)
            {
                throw new Exception("Contract not found.");
            }

            if (contract.ContractStatus == ContractStatus.Expired ||
                contract.ContractStatus == ContractStatus.OnHold)
            {
                throw new Exception("Cannot update a service request for Expired or On Hold contracts.");
            }

            existingRequest.SrDescription = updatedRequest.SrDescription;
            existingRequest.CostForeign = updatedRequest.CostForeign;
            existingRequest.SrStatus = updatedRequest.SrStatus;
            existingRequest.ContractId = updatedRequest.ContractId;

            var rate = await _currencyService.GetToZarRateAsync();
            existingRequest.CostZAR = existingRequest.CostForeign * rate;

            await _context.SaveChangesAsync();
        }

    }
}
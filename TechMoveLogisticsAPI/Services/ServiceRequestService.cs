using TechMoveLogisticsAPI.Models;
using TechMoveLogisticsAPI.Repositories;

namespace TechMoveLogisticsAPI.Services
{
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly IServiceRequestRepo _serviceRequestRepo;
        private readonly IContractRepo _contractRepo;
        private readonly ICurrencyService _currencyService;

        public ServiceRequestService(
            IServiceRequestRepo serviceRequestRepo,
            IContractRepo contractRepo,
            ICurrencyService currencyService)
        {
            _serviceRequestRepo = serviceRequestRepo;
            _contractRepo = contractRepo;
            _currencyService = currencyService;
        }

        public async Task CreateServiceRequestAsync(
            ServiceRequest serviceRequest)
        {
            var contract =
                await _contractRepo.GetByIdAsync(
                    serviceRequest.ContractId);

            if (contract == null)
            {
                throw new Exception("Contract not found");
            }

            if (contract.ContractStatus ==
                    ContractStatus.Expired ||
                contract.ContractStatus ==
                    ContractStatus.OnHold)
            {
                throw new Exception(
                    "Cannot create a service request for Expired or On Hold contracts");
            }

            var rate =
                await _currencyService.GetToZarRateAsync();

            serviceRequest.CostZAR =
                serviceRequest.CostForeign * rate;

            await _serviceRequestRepo.AddAsync(
                serviceRequest);
        }

        public async Task EditServiceRequestAsync(
            ServiceRequest updatedRequest)
        {
            var existingRequest =
                await _serviceRequestRepo.GetByIdAsync(
                    updatedRequest.ServiceRequestId);

            if (existingRequest == null)
            {
                throw new Exception(
                    "Service request not found.");
            }

            var contract =
                await _contractRepo.GetByIdAsync(
                    updatedRequest.ContractId);

            if (contract == null)
            {
                throw new Exception(
                    "Contract not found.");
            }

            if (contract.ContractStatus ==
                    ContractStatus.Expired ||
                contract.ContractStatus ==
                    ContractStatus.OnHold)
            {
                throw new Exception(
                    "Cannot update a service request for Expired or On Hold contracts.");
            }

            existingRequest.SrDescription =
                updatedRequest.SrDescription;

            existingRequest.CostForeign =
                updatedRequest.CostForeign;

            existingRequest.SrStatus =
                updatedRequest.SrStatus;

            existingRequest.ContractId =
                updatedRequest.ContractId;

            var rate =
                await _currencyService.GetToZarRateAsync();

            existingRequest.CostZAR =
                existingRequest.CostForeign * rate;

            await _serviceRequestRepo.UpdateAsync(
                existingRequest);
        }
    }
}
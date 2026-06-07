using TechMoveLogisticsAPI.Models;

namespace TechMoveLogisticsAPI.Services
{
    public interface IServiceRequestService
    {
        Task CreateServiceRequestAsync(ServiceRequest request);

        Task EditServiceRequestAsync(ServiceRequest request);
    }
}

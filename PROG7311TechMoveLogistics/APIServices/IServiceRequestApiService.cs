using PROG7311TechMoveLogistics.Models;

namespace PROG7311TechMoveLogistics.APIServices
{
    public interface IServiceRequestApiService
    {
        Task<List<ServiceRequest>>  GetAllAsync();

        Task<ServiceRequest?> GetByIdAsync(int id);

        Task CreateAsync(  ServiceRequest serviceRequest);

        Task UpdateAsync( ServiceRequest serviceRequest);

        Task DeleteAsync(int id);

    }
}

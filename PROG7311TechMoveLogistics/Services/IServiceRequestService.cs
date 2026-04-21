using PROG7311TechMoveLogistics.Models; 

namespace PROG7311TechMoveLogistics.Services
{
    public interface IServiceRequestService
    {
        //create and edit methods of the service request controller 
        //all code in these these 2 tasks now 
        Task CreateServiceRequest(ServiceRequest serviceRequest);

        Task EditServiceRequest(ServiceRequest serviceRequest);

    }
}

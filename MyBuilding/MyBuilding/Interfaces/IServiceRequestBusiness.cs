using MyBuilding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBuilding.Interfaces
{
    public interface IServiceRequestBusiness
    {
        List<ServiceRequestModel> GetServiceRequests();

        ServiceRequestModel GetServiceRequest(Guid id);

        bool SaveServiceRequest(ServiceRequestModel request);

        bool UpdateServiceRequest(ServiceRequestModel request);

        bool DeleteServiceRequest(Guid id,out bool found);

    }
}

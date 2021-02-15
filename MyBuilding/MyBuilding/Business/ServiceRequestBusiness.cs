using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using MyBuilding.Interfaces;
using MyBuilding.Models;

namespace MyBuilding.Business
{
    public class ServiceRequestBusiness : IServiceRequestBusiness
    {
        private readonly IMemoryCache memoryCache;

        public ServiceRequestBusiness(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }
        public bool DeleteServiceRequest(Guid id,out bool found)
        {
            found = false;
            var data = memoryCache.Get<List<ServiceRequestModel>>("requests");
            if (data != null)
            {
                var obj = data.FirstOrDefault(i => i.Id == id);               
                if (obj != null)
                {
                    found = true;
                    var result=data.Remove(obj);
                    memoryCache.Set("requests", data);
                    return result;
                }
                
            }
            return false;
        }

        public ServiceRequestModel GetServiceRequest(Guid id)
        {
            var data = memoryCache.Get<List<ServiceRequestModel>>("requests");
            if (data != null)
                return data.FirstOrDefault(request => request.Id == id);
            else
                return null;
        }

        public List<ServiceRequestModel> GetServiceRequests()
        {
            var requests= (List<ServiceRequestModel>)memoryCache.Get("requests");
            return requests;
        }

        public bool SaveServiceRequest(ServiceRequestModel request)
        {
            try
            {           
                var data= memoryCache.Get<List<ServiceRequestModel>>("requests");
                if(data == null)
                {
                    data = new List<ServiceRequestModel>();
                    memoryCache.CreateEntry("requests");
                }
                data.Add(request);
                memoryCache.Set("requests", data);
                return true;
            }
            catch (Exception)
            {
                throw;           
            }
        }

        public bool UpdateServiceRequest(ServiceRequestModel request)
        {
            try
            {
                var data = memoryCache.Get<List<ServiceRequestModel>>("requests");
                if (data == null)
                    return false;
                var req = data.FirstOrDefault(i => i.Id == request.Id);
                if (req != null)
                {
                    req.Description = request.Description;
                    req.CurrentStatus = request.CurrentStatus;
                    req.LastModifiedBy = request.LastModifiedBy;
                    req.lastModifiedDate = DateTime.Now;
                    memoryCache.Set("requests", data);
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

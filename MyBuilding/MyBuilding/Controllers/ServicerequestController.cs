using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBuilding.Interfaces;
using MyBuilding.Models;
using MyBuilding.Business;
using System.Net;
using System.Net.Http;
using System.IO;

namespace MyBuilding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicerequestController : ControllerBase
    {
        private readonly IServiceRequestBusiness _servicerequestbusiness;

        public ServicerequestController(IServiceRequestBusiness request)
        {
            _servicerequestbusiness = request;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var requests = _servicerequestbusiness.GetServiceRequests();
                if (requests == null || requests.Count == 0)
                    return NoContent();

                return Ok(requests);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            Guid requestid;
            if (!Guid.TryParse(id,out requestid))
                return BadRequest();

            try
            {
                var request = _servicerequestbusiness.GetServiceRequest(requestid);
                if (request == null)
                    return NotFound();

                return Ok(request);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post(ServiceRequestModel request)
        {         

            try
            {
                request.CreatedDate = DateTime.Now;
                request.lastModifiedDate = DateTime.Now;
                var result = _servicerequestbusiness.SaveServiceRequest(request);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult ServiceRequest(ServiceRequestModel request)
        {
            try
            {
                request.lastModifiedDate = DateTime.Now;
                var result = _servicerequestbusiness.UpdateServiceRequest(request);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult ServiceRequest(string id)
        {
            Guid requestid;
            if (!Guid.TryParse(id, out requestid))
                return BadRequest();

            bool recordfound = false;
            try
            {
                var result = _servicerequestbusiness.DeleteServiceRequest(requestid,out recordfound);
                if (!recordfound)
                    return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
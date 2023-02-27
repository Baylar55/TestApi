using AutoMapper;
using CICWebApi.DTOs;
using CICWebApi.Entities;
using DataAccess.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace CICWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly IPriorityService _priorityService;
        private readonly IRequestTypeService _requestTypeService;
        private readonly IRequestStatusService _requestStatusService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public RequestController(IRequestService requestService,
                                 IPriorityService priorityService,
                                 ICategoryService categoryService,
                                 IRequestTypeService requestTypeService,
                                 IRequestStatusService requestStatusService,
                                 IMapper mapper)
        {
            _mapper = mapper;
            _requestService = requestService;
            _priorityService = priorityService;
            _requestTypeService = requestTypeService;
            _requestStatusService = requestStatusService;
            _categoryService = categoryService;
        }


        #region Documentation
        /// <summary>
        /// Get All Requests
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpGet]
        public IActionResult GetRequests()
        {
            var requests = _mapper.Map<List<RequestDTO>>(_requestService.GetAll());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(requests);
        }


        #region Documentation
        /// <summary>
        /// Get Request by Id
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        #endregion

        [HttpGet("{requestId}")]
        public IActionResult GetRequest(int requestId)
        {
            if (!_requestService.isExist(requestId))
                return NotFound();

            var request = _mapper.Map<RequestDTO>(_requestService.GetById(requestId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(request);
        }


        #region Documentation
        /// <summary>
        /// Create Request
        /// </summary>
        /// <param name="requestTypeId"></param>
        /// <param name="categoryId"></param>
        /// <param name="priorityId"></param>
        /// <param name="requestStatusId"></param>
        /// <param name="RequestCreate"></param>
        /// <returns></returns>
        #endregion

        [HttpPost]
        public IActionResult CreateRequest(int requestTypeId, int categoryId, int priorityId, int requestStatusId, RequestDTO RequestCreate)
        {
            if (RequestCreate == null)
                return BadRequest(ModelState);

            var Requests = _requestService.GetAll()
                .Where(c => c.Title.Trim().ToUpper() == RequestCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (Requests != null)
            {
                ModelState.AddModelError("", "Request already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var RequestMap = _mapper.Map<Request>(RequestCreate);

            RequestMap.Category = _categoryService.GetById(categoryId);
            RequestMap.RequestType = _requestTypeService.GetById(requestTypeId);
            RequestMap.Priority = _priorityService.GetById(priorityId);
            RequestMap.RequestStatus = _requestStatusService.GetById(requestStatusId);

            if (!_requestService.Create(RequestMap))
            {
                ModelState.AddModelError("", "Something went wrong!!!");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        #region Documentation
        /// <summary>
        /// Update Request by ID
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="updatedRequest"></param>
        /// <returns></returns>
        #endregion

        [HttpPut("{requestId}")]
        public IActionResult UpdateRequest(int requestId, RequestDTO updatedRequest)
        {
            if (updatedRequest == null)
                return BadRequest(ModelState);

            if (requestId != updatedRequest.Id)
                return BadRequest(ModelState);

            if (!_requestService.isExist(requestId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var RequestMap = _mapper.Map<Request>(updatedRequest);

            if (!_requestService.Update(RequestMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating Request");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated");
        }


        #region Documentation
        /// <summary>
        /// Delete Request By Id
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        #endregion

        [HttpDelete("{requestId}")]
        public IActionResult DeleteRequest(int requestId)
        {
            if (!_requestService.isExist(requestId))
            {
                return NotFound();
            }

            var requestToDelete = _requestService.GetById(requestId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_requestService.Delete(requestToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting request");
            }

            return Ok("Succesfully removed");
        }


        #region Documentation
        /// <summary>
        /// Delete All Request By Their Category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        #endregion

        [HttpDelete("/DeleteRequestsByCategory/{categoryId}")]
        public IActionResult DeleteRequestsByCategory(int categoryId)
        {
            if (!_categoryService.isExist(categoryId))
                return NotFound();

            var requestsToDelete = _categoryService.GetRequestsByCategory(categoryId).ToList();
            if (!ModelState.IsValid)
                return BadRequest();

            if (!_requestService.DeleteRequests(requestsToDelete))
            {
                ModelState.AddModelError("", "error deleting Requests");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Removed");
        }


        #region Documentation
        /// <summary>
        /// Delete All Request By Their Priority
        /// </summary>
        /// <param name="priorityId"></param>
        /// <returns></returns>
        #endregion

        [HttpDelete("/DeleteRequestsByPriority/{priorityId}")]
        public IActionResult DeleteRequestsByPriority(int priorityId)
        {
            if (!_priorityService.isExist(priorityId))
                return NotFound();

            var requestsToDelete = _priorityService.GetRequestsByPriority(priorityId).ToList();
            if (!ModelState.IsValid)
                return BadRequest();

            if (!_requestService.DeleteRequests(requestsToDelete))
            {
                ModelState.AddModelError("", "error deleting Requests");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Removed");
        }


        #region Documentation
        /// <summary>
        /// Delete All Request By Their Status
        /// </summary>
        /// <param name="requestStatusId"></param>
        /// <returns></returns>
        #endregion

        [HttpDelete("/DeleteRequestsByRequestStatus/{requestStatusId}")]
        public IActionResult DeleteRequestsByRequestStatus(int requestStatusId)
        {
            if (!_requestStatusService.isExist(requestStatusId))
                return NotFound();

            var requestsToDelete = _requestStatusService.GetRequestsByRequestStatus(requestStatusId).ToList();
            if (!ModelState.IsValid)
                return BadRequest();

            if (!_requestService.DeleteRequests(requestsToDelete))
            {
                ModelState.AddModelError("", "error deleting Requests");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Removed");
        }


        #region Documentation
        /// <summary>
        /// Delete All Request By Their Type
        /// </summary>
        /// <param name="requestTypeId"></param>
        /// <returns></returns>
        #endregion

        [HttpDelete("/DeleteRequestsByRequestType/{requestTypeId}")]
        public IActionResult DeleteRequestsByRequestType(int requestTypeId)
        {
            if (!_requestTypeService.isExist(requestTypeId))
                return NotFound();

            var requestsToDelete = _requestTypeService.GetRequestsByRequestType(requestTypeId).ToList();
            if (!ModelState.IsValid)
                return BadRequest();

            if (!_requestService.DeleteRequests(requestsToDelete))
            {
                ModelState.AddModelError("", "error deleting Requests");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Removed");
        }


        #region Get request by his relations


        //#region Documentation
        ///// <summary>
        ///// Get All Requests by Category
        ///// </summary>
        ///// <param name="categoryId"></param>
        ///// <returns></returns>
        //#endregion

        //[HttpGet("request/{categoryId}")]
        //public IActionResult GetRequestsForCategory(int categoryId)
        //{
        //    var requests = _mapper.Map<List<RequestDTO>>(_requestService.GetRequestsOfCategory(categoryId));

        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    return Ok(requests);
        //}


        //#region Documentation
        ///// <summary>
        ///// Get Request by PriorityId
        ///// </summary>
        ///// <param name="priorityId"></param>
        ///// <returns></returns>
        //#endregion

        //[HttpGet("request/{priorityId}")]
        //public IActionResult GetRequestsForPriority(int priorityId)
        //{
        //    var requests = _mapper.Map<List<RequestDTO>>(_requestService.GetRequestsOfPriority(priorityId));

        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    return Ok(requests);
        //}


        //#region Documentation
        ///// <summary>
        ///// Get Request by TypeId
        ///// </summary>
        ///// <param name="requestTypeId"></param>
        ///// <returns></returns>
        //#endregion

        //[HttpGet("request/{requestTypeId}")]
        //public IActionResult GetRequestsForTypes(int requestTypeId)
        //{
        //    var requests = _mapper.Map<List<RequestDTO>>(_requestService.GetRequestsOfType(requestTypeId));

        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    return Ok(requests);
        //}


        //#region Documentation
        ///// <summary>
        ///// Get Requests by Status Id
        ///// </summary>
        ///// <param name="requestStatusId"></param>
        ///// <returns></returns>
        //#endregion

        //[HttpGet("request/{requestStatusId}")]
        //public IActionResult GetRequestsForStatus(int requestStatusId)
        //{
        //    var requests = _mapper.Map<List<RequestDTO>>(_requestService.GetRequestsOfStatus(requestStatusId));

        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    return Ok(requests);
        //}


        #endregion
    }
}

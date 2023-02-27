using AutoMapper;
using CICWebApi.DTOs;
using Data.Entities;
using DataAccess.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace CICWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RequestStatusController : ControllerBase
    {
        private readonly IRequestStatusService _requestStatusService;
        private readonly IMapper _mapper;

        public RequestStatusController(IRequestStatusService RequestStatusService,
                                  IMapper mapper)
        {
            _requestStatusService = RequestStatusService;
            _mapper = mapper;
        }

        #region Documentation
        /// <summary>
        /// Get All RequestStatuses
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpGet]
        public IActionResult GetAll()
        {
            var requestStatuses = _mapper.Map<List<RequestStatusDTO>>(_requestStatusService.GetAll());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(requestStatuses);
        }


        #region Documentation
        /// <summary>
        /// Get RequestStatus By Id
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpGet("{RequestStatusId}")]
        public IActionResult Get(int RequestStatusId)
        {
            if (!_requestStatusService.isExist(RequestStatusId))
                return NotFound();

            var RequestStatus = _mapper.Map<RequestStatusDTO>(_requestStatusService.GetById(RequestStatusId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(RequestStatus);
        }


        #region Documentation
        /// <summary>
        /// Get All Requests By RequestStatusId
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpGet("{RequestStatusId}/requests")]
        public IActionResult GetRequestsByRequestStatus(int RequestStatusId)
        {
            if (!_requestStatusService.isExist(RequestStatusId))
                return NotFound();

            var requests = _mapper.Map<List<RequestDTO>>(
                _requestStatusService.GetRequestsByRequestStatus(RequestStatusId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(requests);
        }


        #region Documentation
        /// <summary>
        /// Create RequestStatus
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpPost]
        public IActionResult CreateRequestStatus([FromBody] RequestStatusDTO RequestStatusCreate)
        {
            if (RequestStatusCreate == null)
                return BadRequest(ModelState);

            //var country = _requestStatusService.GetAll()
            //    .Where(c => c.LastName.Trim().ToUpper() == RequestStatusCreate.LastName.TrimEnd().ToUpper())
            //    .FirstOrDefault();

            //if (country != null)
            //{
            //    ModelState.AddModelError("", "Country already exists");
            //    return StatusCode(422, ModelState);
            //}

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var RequestStatusMap = _mapper.Map<RequestStatus>(RequestStatusCreate);

            if (!_requestStatusService.Create(RequestStatusMap))
            {
                ModelState.AddModelError("", "Something went wrong!!!");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        #region Documentation
        /// <summary>
        /// Update RequestStatus By Id
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpPut("{RequestStatusId}")]
        public IActionResult UpdateRequestStatus(int RequestStatusId, [FromBody] RequestStatusDTO updatedRequestStatus)
        {
            if (updatedRequestStatus == null)
                return BadRequest(ModelState);

            if (RequestStatusId != updatedRequestStatus.Id)
                return BadRequest(ModelState);

            if (!_requestStatusService.isExist(RequestStatusId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var RequestStatusMap = _mapper.Map<RequestStatus>(updatedRequestStatus);

            if (!_requestStatusService.Update(RequestStatusMap))
            {
                ModelState.AddModelError("", "Something went wrong!!!");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }


        #region Documentation
        /// <summary>
        /// Delete RequestStatus By Id
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpDelete("{RequestStatusId}")]
        public IActionResult DeleteRequestStatus(int RequestStatusId)
        {
            if (!_requestStatusService.isExist(RequestStatusId))
            {
                return NotFound();
            }

            var RequestStatusToDelete = _requestStatusService.GetById(RequestStatusId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_requestStatusService.Delete(RequestStatusToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting RequestStatus");
            }

            return Ok();
        }
    }
}

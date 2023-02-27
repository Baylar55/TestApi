using AutoMapper;
using CICWebApi.DTOs;
using CICWebApi.Entities;
using DataAccess.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace CICWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RequestTypeController : ControllerBase
    {

        private readonly IRequestTypeService _requestTypeService;
        private readonly IMapper _mapper;

        public RequestTypeController(IRequestTypeService requestTypeService,
                                  IMapper mapper)
        {
            _requestTypeService = requestTypeService;
            _mapper = mapper;
        }

        #region Documentation
        /// <summary>
        /// Get All RequestTypes
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpGet]
        public IActionResult GetAll()
        {
            var requestTypes = _mapper.Map<List<RequestTypeDTO>>(_requestTypeService.GetAll());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(requestTypes);
        }


        #region Documentation
        /// <summary>
        /// Get RequestType By Id
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpGet("{requestTypeId}")]
        public IActionResult Get(int requestTypeId)
        {
            if (!_requestTypeService.isExist(requestTypeId))
                return NotFound();

            var requestType = _mapper.Map<RequestTypeDTO>(_requestTypeService.GetById(requestTypeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(requestType);
        }


        #region Documentation
        /// <summary>
        /// Get All Requests By RequestTypeId
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpGet("{requestTypeId}/requests")]
        public IActionResult GetRequestsByRequestType(int requestTypeId)
        {
            if (!_requestTypeService.isExist(requestTypeId))
                return NotFound();

            var requests = _mapper.Map<List<RequestDTO>>(
                _requestTypeService.GetRequestsByRequestType(requestTypeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(requests);
        }


        #region Documentation
        /// <summary>
        /// Create RequestType
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpPost]
        public IActionResult CreateRequestType([FromBody] RequestTypeDTO requestTypeCreate)
        {
            if (requestTypeCreate == null)
                return BadRequest(ModelState);

            //var country = _requestTypeService.GetAll()
            //    .Where(c => c.LastName.Trim().ToUpper() == requestTypeCreate.LastName.TrimEnd().ToUpper())
            //    .FirstOrDefault();

            //if (country != null)
            //{
            //    ModelState.AddModelError("", "Country already exists");
            //    return StatusCode(422, ModelState);
            //}

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var requestTypeMap = _mapper.Map<RequestType>(requestTypeCreate);

            if (!_requestTypeService.Create(requestTypeMap))
            {
                ModelState.AddModelError("", "Something went wrong!!!");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        #region Documentation
        /// <summary>
        /// Update RequestType By Id
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpPut("{requestTypeId}")]
        public IActionResult UpdateRequestType(int requestTypeId, [FromBody] RequestTypeDTO updatedrequestType)
        {
            if (updatedrequestType == null)
                return BadRequest(ModelState);

            if (requestTypeId != updatedrequestType.Id)
                return BadRequest(ModelState);

            if (!_requestTypeService.isExist(requestTypeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var requestTypeMap = _mapper.Map<RequestType>(updatedrequestType);

            if (!_requestTypeService.Update(requestTypeMap))
            {
                ModelState.AddModelError("", "Something went wrong!!!");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }


        #region Documentation
        /// <summary>
        /// Delete RequestType By Id
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpDelete("{requestTypeId}")]
        public IActionResult DeleteRequestType(int requestTypeId)
        {
            if (!_requestTypeService.isExist(requestTypeId))
            {
                return NotFound();
            }

            var requestTypeToDelete = _requestTypeService.GetById(requestTypeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_requestTypeService.Delete(requestTypeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting requestType");
            }

            return Ok();
        }
    }
}

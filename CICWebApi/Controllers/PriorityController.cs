using AutoMapper;
using CICWebApi.DTOs;
using CICWebApi.Entities;
using DataAccess.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace CICWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PriorityController : ControllerBase
    {
        private readonly IPriorityService _priorityService;
        private readonly IMapper _mapper;

        public PriorityController(IPriorityService priorityService,
                                  IMapper mapper)
        {
            _priorityService = priorityService;
            _mapper = mapper;
        }

        #region Documentation
        /// <summary>
        /// Get All Priorities
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpGet]
        public IActionResult GetAll()
        {
            var priorities = _mapper.Map<List<PriorityDTO>>(_priorityService.GetAll());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(priorities);
        }


        #region Documentation
        /// <summary>
        /// Get Priority By Id
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpGet("{priorityId}")]
        public IActionResult Get(int priorityId)
        {
            if (!_priorityService.isExist(priorityId))
                return NotFound();

            var priority = _mapper.Map<PriorityDTO>(_priorityService.GetById(priorityId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(priority);
        }


        #region Documentation
        /// <summary>
        /// Get All Requests By PriorityId
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpGet("{priorityId}/requests")]
        public IActionResult GetRequestsByPriority(int priorityId)
        {
            if (!_priorityService.isExist(priorityId))
                return NotFound();

            var requests = _mapper.Map<List<RequestDTO>>(
                _priorityService.GetRequestsByPriority(priorityId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(requests);
        }


        #region Documentation
        /// <summary>
        /// Create Priority
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpPost]
        public IActionResult CreatePriority([FromBody] PriorityDTO priorityCreate)
        {
            if (priorityCreate == null)
                return BadRequest(ModelState);

            //var country = _priorityService.GetAll()
            //    .Where(c => c.LastName.Trim().ToUpper() == priorityCreate.LastName.TrimEnd().ToUpper())
            //    .FirstOrDefault();

            //if (country != null)
            //{
            //    ModelState.AddModelError("", "Country already exists");
            //    return StatusCode(422, ModelState);
            //}

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var priorityMap = _mapper.Map<Priority>(priorityCreate);

            if (!_priorityService.Create(priorityMap))
            {
                ModelState.AddModelError("", "Something went wrong!!!");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        #region Documentation
        /// <summary>
        /// Update Priority By Id
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpPut("{priorityId}")]
        public IActionResult UpdatePriority(int priorityId, [FromBody] PriorityDTO updatedpriority)
        {
            if (updatedpriority == null)
                return BadRequest(ModelState);

            if (priorityId != updatedpriority.Id)
                return BadRequest(ModelState);

            if (!_priorityService.isExist(priorityId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var priorityMap = _mapper.Map<Priority>(updatedpriority);

            if (!_priorityService.Update(priorityMap))
            {
                ModelState.AddModelError("", "Something went wrong!!!");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }


        #region Documentation
        /// <summary>
        /// Delete Priority By Id
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpDelete("{priorityId}")]
        public IActionResult DeletePriority(int priorityId)
        {
            if (!_priorityService.isExist(priorityId))
            {
                return NotFound();
            }

            var priorityToDelete = _priorityService.GetById(priorityId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_priorityService.Delete(priorityToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting priority");
            }

            return Ok();
        }
    }
}

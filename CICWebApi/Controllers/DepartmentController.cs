using AutoMapper;
using CICWebApi.DTOs;
using Data.Entities;
using DataAccess.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace CICWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService,
                                  IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        #region Documentation
        /// <summary>
        /// Get All Departments
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpGet]
        public IActionResult GetAll()
        {
            var departments = _mapper.Map<List<DepartmentDTO>>(_departmentService.GetAll());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(departments);
        }


        #region Documentation
        /// <summary>
        /// Get Department By Id
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpGet("{departmentId}")]
        public IActionResult Get(int departmentId)
        {
            if (!_departmentService.isExist(departmentId))
                return NotFound();

            var department = _mapper.Map<DepartmentDTO>(_departmentService.GetById(departmentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(department);
        }


        #region Documentation
        /// <summary>
        /// Get All Users By departmentId
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpGet("{departmentId}/users")]
        public IActionResult GetUsersBydepartment(int departmentId)
        {
            if (!_departmentService.isExist(departmentId))
                return NotFound();

            var users = _mapper.Map<List<UserDTO>>(
                _departmentService.GetUsersByDepartment(departmentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }


        #region Documentation
        /// <summary>
        /// Create Department
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpPost]
        public IActionResult CreateDepartment([FromBody] DepartmentDTO departmentCreate)
        {
            if (departmentCreate == null)
                return BadRequest(ModelState);

            //var country = _departmentService.GetAll()
            //    .Where(c => c.LastName.Trim().ToUpper() == departmentCreate.LastName.TrimEnd().ToUpper())
            //    .FirstOrDefault();

            //if (country != null)
            //{
            //    ModelState.AddModelError("", "Country already exists");
            //    return StatusCode(422, ModelState);
            //}

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var departmentMap = _mapper.Map<Department>(departmentCreate);

            if (!_departmentService.Create(departmentMap))
            {
                ModelState.AddModelError("", "Something went wrong!!!");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        #region Documentation
        /// <summary>
        /// Update department By Id
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpPut("{departmentId}")]
        public IActionResult UpdateDepartment(int departmentId, [FromBody] DepartmentDTO updateddepartment)
        {
            if (updateddepartment == null)
                return BadRequest(ModelState);

            if (departmentId != updateddepartment.Id)
                return BadRequest(ModelState);

            if (!_departmentService.isExist(departmentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var departmentMap = _mapper.Map<Department>(updateddepartment);

            if (!_departmentService.Update(departmentMap))
            {
                ModelState.AddModelError("", "Something went wrong!!!");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }


        #region Documentation
        /// <summary>
        /// Delete Department By Id
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpDelete("{departmentId}")]
        public IActionResult Deletedepartment(int departmentId)
        {
            if (!_departmentService.isExist(departmentId))
            {
                return NotFound();
            }

            var departmentToDelete = _departmentService.GetById(departmentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_departmentService.Delete(departmentToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting department");
            }

            return Ok();
        }
    }
}

using AutoMapper;
using CICWebApi.DTOs;
using CICWebApi.Entities;
using DataAccess.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace CICWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService,
                                  IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        #region Documentation
        /// <summary>
        /// Get All Categories
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _mapper.Map<List<CategoryDTO>>(_categoryService.GetAll());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }


        #region Documentation
        /// <summary>
        /// Get Category By Id
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpGet("{categoryId}")]
        public IActionResult Get(int categoryId)
        {
            if (!_categoryService.isExist(categoryId))
                return NotFound();

            var category = _mapper.Map<CategoryDTO>(_categoryService.GetById(categoryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }


        #region Documentation
        /// <summary>
        /// Get All Requests By CategoryId
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpGet("{categoryId}/requests")]
        public IActionResult GetRequestsByCategory(int categoryId)
        {
            if (!_categoryService.isExist(categoryId))
                return NotFound();

            var requests = _mapper.Map<List<RequestDTO>>(
                _categoryService.GetRequestsByCategory(categoryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(requests);
        }


        #region Documentation
        /// <summary>
        /// Create Category
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpPost]
        public IActionResult Createcategory([FromBody] CategoryDTO categoryCreate)
        {
            if (categoryCreate == null)
                return BadRequest(ModelState);

            //var country = _categoryService.GetAll()
            //    .Where(c => c.LastName.Trim().ToUpper() == categoryCreate.LastName.TrimEnd().ToUpper())
            //    .FirstOrDefault();

            //if (country != null)
            //{
            //    ModelState.AddModelError("", "Country already exists");
            //    return StatusCode(422, ModelState);
            //}

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(categoryCreate);

            if (!_categoryService.Create(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong!!!");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        #region Documentation
        /// <summary>
        /// Update Category By Id
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpPut("{categoryId}")]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDTO updatedcategory)
        {
            if (updatedcategory == null)
                return BadRequest(ModelState);

            if (categoryId != updatedcategory.Id)
                return BadRequest(ModelState);

            if (!_categoryService.isExist(categoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var categoryMap = _mapper.Map<Category>(updatedcategory);

            if (!_categoryService.Update(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong!!!");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }


        #region Documentation
        /// <summary>
        /// Delete Category By Id
        /// </summary>
        /// <returns></returns>
        #endregion

        [HttpDelete("{categoryId}")]
        public IActionResult Deletecategory(int categoryId)
        {
            if (!_categoryService.isExist(categoryId))
            {
                return NotFound();
            }

            var categoryToDelete = _categoryService.GetById(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoryService.Delete(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return Ok();
        }
    }
}

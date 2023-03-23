using API.Model;
using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/Category
        [HttpGet]
        public IActionResult Get()
        {
            var categories = _categoryRepository.GetGategories();
            return new OkObjectResult(categories);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            return new OkObjectResult(category);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            if (category == null)
            {
                return new NoContentResult();
            }
            using (var scope = new TransactionScope())
            {
                _categoryRepository.InsertCategory(category);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
            }
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Category category)
        {
            if (category == null)
            {
                return new NoContentResult();
            }
            using (var scope = new TransactionScope())
            {
                _categoryRepository.UpdateCategory(category);
                scope.Complete();
                return new OkResult();
            }
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _categoryRepository.DeleteCategory(id);
            return new OkResult();
        }
    }
}

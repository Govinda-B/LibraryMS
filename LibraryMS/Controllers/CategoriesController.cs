using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using LibraryMS.BAL;
using LoggerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        //private readonly IRepositoryManager _repository;
        //private readonly ILoggerManager _logger;
        //private readonly IMapper _mapper;
        private readonly ICategoriesBAL _categoriesBAL;

        //public CategoriesController(ICategoriesBAL categoriesBAL, IRepositoryManager repository, ILoggerManager logger,IMapper mapper)
        public CategoriesController(ICategoriesBAL categoriesBAL)
        {
            _categoriesBAL = categoriesBAL;
            //_repository = repository;
            //_logger = logger;
            //_mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            //var categories = await _repository.Category.GetAllCategoriesAsync();
            //var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            //return Ok(categoriesDto);

            return Ok(await _categoriesBAL.GetAllCategories());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            //var category = await _repository.Category.GetCategoryAsync(id);
            //var categoryDto = _mapper.Map<CategoryDto>(category);
            //if (categoryDto == null)
            //{
            //    _logger.LogInfo($"Book with id: {id} doesn't exist in the database.");
            //    return NotFound();
            //}
            //else
            //{
            //    return Ok(categoryDto);
            //}
            CategoryDto categoryDto = await _categoriesBAL.GetCategoryAsync(id);
            if (categoryDto==null)
            {
                return NotFound();
            }
            return Ok(categoryDto);
            //return await _repository.Category.GetCategoryAsync(id);
        }
        
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody]CategoryCreateDto category)
        {
            Category categoryEntity = await _categoriesBAL.CreateCategory(category);
            if (categoryEntity == null)
            {
                //_logger.LogError("category sent from client is null.");
                return BadRequest("category object is null");
            }
            //var categoryEntity = _mapper.Map<Category>(category);
            //_repository.Category.CreateCategory(categoryEntity);
            //await _repository.SaveAsync();
            return Created("Successfully Created",categoryEntity);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            bool result = await _categoriesBAL.DeleteCategory(id);
            if (result)
                return Ok();
            return NotFound("Category for given id does not exist");

            //Category category = await _repository.Category.GetCategoryAsync(id);
            //if (category == null)
            //{
            //    _logger.LogInfo($"Category with id: {id} doesn't exist in the database.");
            //    //return NotFound("The category record couldn't be found.");
            //    return new ObjectResult("The category record couldn't be found.");
            //}
            //_repository.Category.DeleteCategory(category);
            //await _repository.SaveAsync();
            //return  Ok();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryCreateDto category)
        {
            string result = await _categoriesBAL.UpdateCategoryAsync(id, category);
            if (result!= null)
                return NotFound(result);
            return Ok("Updated");

            //if (category == null)
            //{
            //    return NotFound("category is null.");
            //}
            //Category categoryToUpdate = await _repository.Category.GetCategoryAsync(id);
            //if (categoryToUpdate == null)
            //{
            //    return NotFound("The category record couldn't be found.");
            //}
            //_mapper.Map(category, categoryToUpdate);
            //_repository.Save();
            //return Ok();
        }
    }
}

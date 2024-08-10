using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Dtos.Category;
using server.Mappers;

namespace server.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext dbContext)
        {
            _context = dbContext;


        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _context.Categories.ToList().Select(s => s.ToCategoryDto());
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category.ToCategoryDto());
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateCategoryDto categoryDto)
        {
            var categoryModel = categoryDto.FromCreateDTO();
            _context.Categories.Add(categoryModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = categoryModel.Id }, categoryModel.ToCategoryDto());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromBody] UpdateCategoryDto categoryDto, [FromRoute] int id)
        {

            var categoryModel = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (categoryModel == null)
            {
                return NotFound();
            }
            categoryModel.Description = categoryDto.Description;
            categoryModel.Name = categoryDto.Name;

            _context.SaveChanges();
            return Ok(categoryModel.ToCategoryDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var categoryModel = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categoryModel);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
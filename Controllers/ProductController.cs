using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Dtos.Product;
using server.Mappers;

namespace server.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _context.Products.ToList().Select(p => p.ToProductDto());

            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();
            return Ok(product.ToProductDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateProductDto productDto)
        {
            var productModel = productDto.FromCreateDto();
            _context.Add(productDto);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = productModel.Id }, productModel.ToProductDto());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateProductDto productDto)
        {
            var productModel = _context.Products.FirstOrDefault((p) => p.Id == id);
            if (productModel == null)
            {
                return NotFound();
            }

            productModel.Description = productDto.Description;
            productModel.ProductName = productDto.Name;
            productModel.Quantity = productDto.Quantity;
            productModel.Price = productDto.Price;
            productModel.CategoryId = productDto.CategoryId;

            _context.SaveChanges();
            return Ok(productModel.ToProductDto());

        }
    }
}
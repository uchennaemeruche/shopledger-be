using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Dtos.Category;
using server.Dtos.Product;
using server.Models;

namespace server.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToProductDto(this Product productModel)
        {
            var dto = new ProductDto
            {
                Id = productModel.Id,
                Name = productModel.ProductName,
                Description = productModel.Description,
                Price = productModel.Price,
                Quantity = productModel.Quantity,


            };
            if (productModel.Category != null)
            {
                dto.Category = new CategoryDto
                {
                    Id = productModel.Category.Id,
                    Description = productModel.Category.Description,
                    Name = productModel.Category.Name
                };
            }
            return dto;
        }
        public static Product FromCreateDto(this CreateProductDto productDto)
        {
            return new Product
            {
                ProductName = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Quantity = productDto.Quantity,
                CategoryId = productDto.CategoryId

            };


        }
    }

}
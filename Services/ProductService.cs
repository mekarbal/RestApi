using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication2.Exceptions;
using WebApplication2.Models;
using WebApplication2.Models.DTOs.product;
using WebApplication2.Repositories;
using WebApplication2.Responses;

namespace WebApplication2.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async  Task<Product> AddProductAsync(ProductCreateDTO productCreateDto)
        {
            var product = new Product
            {
                Name = productCreateDto.Name,
                Description = productCreateDto.Description,
                Price = productCreateDto.Price,
                Ingredients = productCreateDto.Ingredients
            };

            await _productRepository.AddProductAsync(product);

            return product;
        }

        public async Task DeleteProductAsync(int id)
        {
            try
            {

                Product product=await _productRepository.GetProductByIdAsync(id);


                if (product == null) {
                    throw new NotFoundException("Product");
                }
                await _productRepository.DeleteProductAsync(product);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<Product>> GetProductsAsync(int pageNumber, int pageSize, string nameFilter, string ingredientsFilter)
        {
            return await _productRepository.GetProductsAsync(pageNumber, pageSize, nameFilter, ingredientsFilter);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {

            try
            {
                Product productFound = await _productRepository.GetProductByIdAsync(id);

                if (productFound == null)
                {

                    throw new NotFoundException($"{id}");
                }

                return productFound;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Product> UpdateProductAsync(ProductUpdateDTO productDto, int id)
        {
            try
            {
                Product productFound = await _productRepository.GetProductByIdAsync(id);

            if (productFound == null)
            {

                throw new NotFoundException($"{id}");
            }

                productFound.Name = productDto.Name;
                productFound.Description = productDto.Description;  
                productFound.Price = productDto.Price;
                productFound.Ingredients = productDto.Ingredients;
                return await _productRepository.UpdateProductAsync(productFound, productFound.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

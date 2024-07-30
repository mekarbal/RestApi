using Microsoft.AspNetCore.Mvc;
using WebApplication2.Enums;
using WebApplication2.Exceptions;
using WebApplication2.Models;
using WebApplication2.Models.DTOs.product;
using WebApplication2.Repositories;
using WebApplication2.Responses;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10, 
            [FromQuery] string nameFilter = "", 
            [FromQuery] string ingredientsFilter=""
            )
        {
            try
            {
                IEnumerable<Product> products = await _productService.GetProductsAsync(pageNumber, pageSize, nameFilter, ingredientsFilter);

                if (products == null || !products.Any())
                {
                    return NotFound(new ResponseData<object>(ProductEnums.NOT_FOUND, null));
                }

                return Ok(new ResponseData<IEnumerable<Product>>(ProductEnums.ALL_PRODUCTS, products));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ResponseData<object>(ex.Message, null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseData<object>(GeneralErrors.UNEXPECTED_ERROR, null));
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> InsertProduct([FromBody] ProductCreateDTO product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _productService.AddProductAsync(product);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> getOneProduct(int id)
        {
            try
            {
                Product product = await _productService.GetProductByIdAsync(id);

                return Ok(new ResponseData<Product>(ProductEnums.GET_ONE_PRODUCT, product));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ResponseData<object>(ex.Message, null));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResponseData<object>(GeneralErrors.UNEXPECTED_ERROR, null));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);

                return Ok(new ResponseData<Product>(ProductEnums.DELETE_PRODUCT, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ResponseData<object>(ex.Message, null));

            }
            catch (Exception)
            {
                return StatusCode(500, new ResponseData<object>(GeneralErrors.UNEXPECTED_ERROR, null));
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> UpdateProduct([FromBody] ProductUpdateDTO product, int id)
        {
            try
            {
                Product productUpdated =await _productService.UpdateProductAsync(product,id);

                return Ok(new ResponseData<Product>(ProductEnums.UPDATE_PRODUCT, productUpdated));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ResponseData<object>(ex.Message, null));

            }
            catch (Exception)
            {
                return StatusCode(500, new ResponseData<object>(GeneralErrors.UNEXPECTED_ERROR, null));
            }
        }
    }
}

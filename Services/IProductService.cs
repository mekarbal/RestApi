using WebApplication2.Models;
using WebApplication2.Models.DTOs.product;

namespace WebApplication2.Repositories
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync(int pageNumber, int pageSize, string nameFilter, string ingredientsFilter);
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> AddProductAsync(ProductCreateDTO product);
        Task<Product> UpdateProductAsync(ProductUpdateDTO product,int id);
        Task DeleteProductAsync(int id);
    }
}

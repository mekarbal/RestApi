using WebApplication2.Models;
using WebApplication2.Models.DTOs.product;

namespace WebApplication2.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(int pageNumber, int pageSize, string nameFilter, string ingredientsFilter);
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product, int id);
        Task DeleteProductAsync(Product product);
    }
}

using Microsoft.EntityFrameworkCore;
using WebApplication2.ConnectionContext;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{


    public class ProductRepository : IProductRepository
    {

        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int pageNumber, int pageSize, string nameFilter, string ingredientsFilter)
        {
            IQueryable<Product> query = _context.Products;

            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                query = query
                            .Where(p => p.Name.Contains(nameFilter));
            }

            var products = await query.ToListAsync();

            if (!string.IsNullOrWhiteSpace(ingredientsFilter))
            {
                products = products
                                  .Where(p => p.Ingredients != null && p.Ingredients.Any(i => i.Contains(ingredientsFilter))).ToList();
            }

            return products.Skip((pageNumber - 1) * pageSize)
                           .Take(pageSize)
                           .ToList();
        }


        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> UpdateProductAsync(Product product, int id)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}

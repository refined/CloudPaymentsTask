using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudPayments.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudPayments.DataServices
{
    public interface IProductsRepository
    {
        Task<Product> GetByIdAsync(int id);
        Task<List<Product>> ListAsync();
        Task AddAsync(Product session);
        Task UpdateAsync(Product session);
    }

    public class ProductsRepository : IProductsRepository
    {
        private readonly CloudPaymentsDbContext _dbContext;

        public ProductsRepository(CloudPaymentsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Product> GetByIdAsync(int id)
        {
            return _dbContext.Products.FirstOrDefaultAsync<Product>(s => s.Id == id);
        }

        public Task<List<Product>> ListAsync()
        {
            return _dbContext.Products
                .OrderByDescending<Product, double>(s => s.Price)
                .ToListAsync();
        }

        public Task AddAsync(Product item)
        {
            _dbContext.Products.Add(item);
            return _dbContext.SaveChangesAsync();
        }

        public Task UpdateAsync(Product item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
            return _dbContext.SaveChangesAsync();
        }
    }
}
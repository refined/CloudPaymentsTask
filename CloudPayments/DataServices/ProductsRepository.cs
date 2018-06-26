using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CloudPayments.Models;
using CloudPayments.Services;
using Microsoft.EntityFrameworkCore;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace CloudPayments.DataServices
{
    public interface IProductsRepository
    {
        Task<Product> GetByIdAsync(int id);
        Task<List<Product>> ListAsync();
        Task AddAsync(Product item);
        Task UpdateAsync(Product item);
        Task RemoveAsync(int id);
    }

    public class ProductsRepository : IProductsRepository
    {
        private readonly CloudPaymentsDbContext _dbContext;
        private readonly IImageService _imageService;

        public ProductsRepository(CloudPaymentsDbContext dbContext, IImageService imageService)
        {
            _dbContext = dbContext;
            _imageService = imageService;
        }

        public Task<Product> GetByIdAsync(int id)
        {
            return _dbContext.Products.FirstOrDefaultAsync<Product>(s => s.Id == id);
        }

        public Task<List<Product>> ListAsync()
        {
            return _dbContext.Products
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

        public async Task RemoveAsync(int id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                throw new ObjectNotFoundException();
            }
            _dbContext.Products.Remove(product);
            await _imageService.DeleteImage(product.ImageName);
            await _dbContext.SaveChangesAsync();
        }
    }
}
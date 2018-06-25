using CloudPayments.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudPayments.DataServices
{
    public class CloudPaymentsDbContext : DbContext
    {
        public CloudPaymentsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }

}

using CloudPayments.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudPayments.DataService
{
    public class CloudPaymentsDbContext : DbContext
    {
        public CloudPaymentsDbContext()
        {
        }

        public DbSet<Product> Products { get; set; }
    }

}

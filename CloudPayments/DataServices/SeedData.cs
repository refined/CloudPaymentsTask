using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudPayments.DataServices;
using CloudPayments.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CloudPayments.DataServices
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CloudPaymentsDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<CloudPaymentsDbContext>>()))
            {
                if (context.Products.Any())
                {
                    return;  
                }

                context.Products.AddRange(
                    new Product
                    {
                        Title = "Стиральная машина",
                        Price = 9000,
                        Currency = "р.",
                        ImageName = "products/washer.jpg"
                    },
                    new Product
                    {
                        Title = "Пылесос",
                        Price = 4999.99,
                        Currency = "р",
                        ImageName = "products/vac.jpg"
                    },
                    new Product
                    {
                        Title = "Холодильник",
                        Price = 8000,
                        Currency = "руб",
                        ImageName = "products/fridge.jpg"
                    },
                    new Product
                    {
                        Title = "Стиралка за доллары",
                        Price = 800,
                        Currency = "usd",
                        ImageName = "products/washer2.jpg"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CloudPayments.DataServices;
using Microsoft.AspNetCore.Mvc;
using CloudPayments.Models;
using CloudPayments.Services;

namespace CloudPayments.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ICurrencyManager _currencyManager;

        public HomeController(IProductsRepository productsRepository, ICurrencyManager currencyManager)
        {
            _productsRepository = productsRepository;
            _currencyManager = currencyManager;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productsRepository.ListAsync();
            return View(products.Select(p => new ProductViewModel(p, _currencyManager)));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

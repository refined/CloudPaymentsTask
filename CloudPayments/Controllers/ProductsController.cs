using System;
using System.Threading.Tasks;
using CloudPayments.DataService;
using CloudPayments.DataServices;
using CloudPayments.Models;
using Microsoft.AspNetCore.Mvc;

namespace CloudPayments.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productsRepository.ListAsync();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dbModel = await _productsRepository.GetByIdAsync(id);
            if (dbModel == null)
            {
                return NotFound(id);
            }

            return View(dbModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Price,Currency")] Product model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productsRepository.AddAsync(model);

            //return Ok();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbModel = await _productsRepository.GetByIdAsync(model.Id);
            if (dbModel == null)
            {
                return NotFound(model.Id);
            }
            try
            {
                dbModel.Price = model.Price;
                dbModel.Title = model.Title;
                dbModel.Currency = model.Currency;
                dbModel.ImageName = model.ImageName;
                await _productsRepository.UpdateAsync(dbModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

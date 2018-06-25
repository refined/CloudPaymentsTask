using System;
using System.IO;
using System.Threading.Tasks;
using CloudPayments.DataServices;
using CloudPayments.Models;
using CloudPayments.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CloudPayments.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IImageService _imageService;

        public ProductsController(IProductsRepository productsRepository, IImageService imageService)
        {
            _productsRepository = productsRepository;
            _imageService = imageService;
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
        public async Task<IActionResult> Create([Bind("Title,Price,Currency")] Product model, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }
            var filePath = await _imageService.SaveImage(file, "products");
            
            model.ImageName = filePath;
            await _productsRepository.AddAsync(model);

            //return Ok();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product model, IFormFile file)
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
                var newImagePath = await _imageService.SaveImage(file, "products");
                dbModel.ImageName = newImagePath;
                dbModel.Price = model.Price;
                dbModel.Title = model.Title;
                dbModel.Currency = model.Currency;
                
                await _productsRepository.UpdateAsync(dbModel);
                if (!string.IsNullOrEmpty(dbModel.ImageName))
                {
                    await _imageService.DeleteImage(dbModel.ImageName);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _productsRepository.RemoveAsync(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

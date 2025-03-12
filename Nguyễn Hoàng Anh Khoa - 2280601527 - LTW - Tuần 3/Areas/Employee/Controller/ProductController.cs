using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nguyễn_Hoàng_Anh_Khoa___2280601527___LTW___Tuần_3.Models;
using Nguyễn_Hoàng_Anh_Khoa___2280601527___LTW___Tuần_3.Repositories;

namespace Nguyễn_Hoàng_Anh_Khoa___2280601527___LTW___Tuần_3.Areas.Employee.Controllers;
    [Area("Employee")]
    [Authorize(Roles = "Employee")]// Chỉ Employee được phép truy cập
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        // Employee có thể xem danh sách sản phẩm
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }

        // Employee có thể xem chi tiết sản phẩm
        public async Task<IActionResult> Display(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // Employee có thể chỉnh sửa sản phẩm
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Product product, IFormFile? imageUrl)
        {
            if (id != product.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Name");
                return View(product);
            }

            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
                return NotFound();

            if (imageUrl != null)
                existingProduct.ImageUrl = await SaveImage(imageUrl);

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;
            existingProduct.CategoryId = product.CategoryId;

            await _productRepository.UpdateAsync(existingProduct);
            return RedirectToAction(nameof(Index));
        }

        private async Task<string> SaveImage(IFormFile image)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{image.FileName}";
            var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return "/images/" + uniqueFileName;
        }
    }

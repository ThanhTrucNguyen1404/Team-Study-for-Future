using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nguyễn_Hoàng_Anh_Khoa___2280601527___LTW___Tuần_3.Models;
using Nguyễn_Hoàng_Anh_Khoa___2280601527___LTW___Tuần_3.Repositories;
using System.Linq;
using System.Security.Claims;

namespace Nguyễn_Hoàng_Anh_Khoa___2280601527___LTW___Tuần_3.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        // ✅ Lấy vai trò của người dùng hiện tại và truyền vào View
        private string GetUserRole()
        {
            if (!User.Identity.IsAuthenticated) return "Khách";
            var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            return roles.Any() ? string.Join(", ", roles) : "Người dùng";
        }

        // ✅ Bất kỳ ai cũng có thể xem danh sách sản phẩm
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            ViewBag.UserRole = GetUserRole();
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }

        // ✅ Bất kỳ ai cũng có thể xem chi tiết sản phẩm
        [AllowAnonymous]
        public async Task<IActionResult> Display(int id)
        {
            ViewBag.UserRole = GetUserRole();
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // ✅ Chỉ Admin được thêm sản phẩm
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {
            ViewBag.UserRole = GetUserRole();
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(Product product, IFormFile? imageUrl)
        {
            ViewBag.UserRole = GetUserRole();
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Name");
                return View(product);
            }

            product.ImageUrl = imageUrl != null ? await SaveImage(imageUrl) : "/images/default.jpg";
            await _productRepository.AddAsync(product);
            return RedirectToAction(nameof(Index));
        }

        // ✅ Admin và Employee có thể chỉnh sửa sản phẩm
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.UserRole = GetUserRole();
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Update(int id, Product product, IFormFile? imageUrl)
        {
            ViewBag.UserRole = GetUserRole();
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

        // ✅ Chỉ Admin được xóa sản phẩm
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            ViewBag.UserRole = GetUserRole();
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // ✅ Gợi ý tìm kiếm mở cho tất cả người dùng (bao gồm khách chưa đăng nhập)
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetSuggestions(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return Json(new List<object>());

            var suggestions = (await _productRepository.SearchProductsAsync(search))
                .Select(p => new { p.Id, p.Name })
                .Take(5)
                .ToList();

            return Json(suggestions);
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
}

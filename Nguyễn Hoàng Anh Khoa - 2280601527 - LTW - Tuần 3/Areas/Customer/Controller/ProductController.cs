using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nguyễn_Hoàng_Anh_Khoa___2280601527___LTW___Tuần_3.Models;
using Nguyễn_Hoàng_Anh_Khoa___2280601527___LTW___Tuần_3.Repositories;

namespace Nguyễn_Hoàng_Anh_Khoa___2280601527___LTW___Tuần_3.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Customer")] // Chỉ Customer được phép truy cập
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Hiển thị danh sách sản phẩm (Customer có thể xem)
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }

        // Hiển thị chi tiết sản phẩm
        public async Task<IActionResult> Display(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}

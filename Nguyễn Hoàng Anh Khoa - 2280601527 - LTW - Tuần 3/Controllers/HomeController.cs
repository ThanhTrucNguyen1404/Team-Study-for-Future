using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nguyễn_Hoàng_Anh_Khoa___2280601527___LTW___Tuần_3.Models;

namespace Nguyễn_Hoàng_Anh_Khoa___2280601527___LTW___Tuần_3.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

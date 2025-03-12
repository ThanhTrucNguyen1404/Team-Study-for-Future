using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nguyễn_Hoàng_Anh_Khoa___2280601527___LTW___Tuần_3.Models;
using Nguyễn_Hoàng_Anh_Khoa___2280601527___LTW___Tuần_3.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Cấu hình Identity (Không yêu cầu xác nhận email)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = false; // Không cần xác nhận email trước khi đăng nhập
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders()
.AddDefaultUI();

// Cấu hình Cookie Authentication
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

// Thêm dịch vụ Razor Pages và Controllers
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// Đăng ký các Repository
builder.Services.AddScoped<IProductRepository, EFProductRepository>();
builder.Services.AddScoped<ICategoryRepository, EFCategoryRepository>();

var app = builder.Build();

// Cấu hình middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Cấu hình các endpoint
app.MapRazorPages();
app.MapControllers();

app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "product",
    pattern: "Product/{action=Index}/{id?}");

// 🚀 Chạy ứng dụng
app.Run();

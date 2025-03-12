using Microsoft.EntityFrameworkCore;
using Nguyễn_Hoàng_Anh_Khoa___2280601527___LTW___Tuần_3.Models;

namespace Nguyễn_Hoàng_Anh_Khoa___2280601527___LTW___Tuần_3.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public EFProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            // return await _context.Products.ToListAsync(); 
            return await _context.Products.Include(p => p.Category).ToListAsync();

        }

        public async Task<Product> GetByIdAsync(int id)
        {
            // return await _context.Products.FindAsync(id); 
            // lấy thông tin kèm theo category 
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string search)
        {
            return await _context.Products
                .Where(p => p.Name.Contains(search)) // Tìm sản phẩm theo tên
                .Take(5)
                .ToListAsync();
        }
    }
}


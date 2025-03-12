using Nguyễn_Hoàng_Anh_Khoa___2280601527___LTW___Tuần_3.Models;

namespace Nguyễn_Hoàng_Anh_Khoa___2280601527___LTW___Tuần_3.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<IEnumerable<Product>> SearchProductsAsync(string search);
    }
}

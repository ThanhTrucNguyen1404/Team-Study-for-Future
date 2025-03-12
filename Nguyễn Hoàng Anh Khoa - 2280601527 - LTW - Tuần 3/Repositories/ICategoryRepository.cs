using Nguyễn_Hoàng_Anh_Khoa___2280601527___LTW___Tuần_3.Models;

namespace Nguyễn_Hoàng_Anh_Khoa___2280601527___LTW___Tuần_3.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
    }
}

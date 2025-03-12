using System.ComponentModel.DataAnnotations;

namespace Nguyễn_Hoàng_Anh_Khoa___2280601527___LTW___Tuần_3.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}

namespace Nguyễn_Hoàng_Anh_Khoa___2280601527___LTW___Tuần_3.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}

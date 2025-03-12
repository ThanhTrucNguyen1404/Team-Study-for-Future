using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Nguyễn_Hoàng_Anh_Khoa___2280601527___LTW___Tuần_3.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }   
        public string? Address { get; set; }
        public string? Age { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using WebApplication2.Enums;

namespace WebApplication2.Models.DTOs.product
{
    public class ProductCreateDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage =ProductEnums.REQUIRED_PRICE)]
        public double Price { get; set; }

        public string[] Ingredients { get; set; }

    }
}

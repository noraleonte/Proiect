using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.Models
{
    public class Product
    {
        public int ID { get; set; }

        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Numele produsului trebuie sa contina doar cuvinte si spatii"), Required, StringLength(50, MinimumLength = 3)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Range(1, 300)]
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int ManufacturerID { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}

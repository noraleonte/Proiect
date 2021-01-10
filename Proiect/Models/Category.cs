using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.Models
{
    public class Category
    {
        public int ID { get; set; }
        [RegularExpression(@"^[A-Z][A-Za-z\s]+$", ErrorMessage = "Numele categoriei trebuie sa inceapa cu litera mare si sa contina doar litere si spatii"), Required, StringLength(20, MinimumLength = 3)]
        public string CategoryName { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}

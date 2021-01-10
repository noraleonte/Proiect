using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.Models
{
    public class Manufacturer
    {
        public int ID { get; set; }

        [RegularExpression(@"^[A-Z][a-z]+\s[A-Z][a-z]+$", ErrorMessage = "Producatorului trebuie sa contina doar liere si spatii"), Required, StringLength(50, MinimumLength = 3)]
        public string ManufacturerName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}

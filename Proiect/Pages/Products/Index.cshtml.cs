using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public IndexModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }
        public ProductData ProductD { get; set; }
        public int ProductID { get; set; }
        public int CategoryID { get; set; }

        public async Task OnGetAsync(int? id, int? categoryID)
        {
            ProductD = new ProductData();

            Product = await _context.Product
                .Include(p => p.Manufacturer).Include(p => p.ProductCategories).ThenInclude(p => p.Category).AsNoTracking().OrderBy(p => p.Name).ToListAsync();
            if (id != null) { 
                ProductID = id.Value;
                Product product = ProductD.Products.Where(i => i.ID == id.Value).Single(); 
                ProductD.Categories = product.ProductCategories.Select(s => s.Category); }
        }
    }
}

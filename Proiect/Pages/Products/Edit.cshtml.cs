using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Products
{
    public class EditModel : ProductCategoriesPageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public EditModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product.Include(p => p.Manufacturer).Include(p => p.ProductCategories).ThenInclude(p => p.Category).AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);

            if (Product == null)
            {
                return NotFound();
            }
            PopulateAssignedCategoryData(_context, Product);
            ViewData["ManufacturerID"] = new SelectList(_context.Set<Manufacturer>(), "ID", "ManufacturerName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null) { 
                return NotFound();
            }
            var productToUpdate = await _context.Product.Include(i => i.Manufacturer).Include(i => i.ProductCategories).ThenInclude(i => i.Category).FirstOrDefaultAsync(s => s.ID == id);
            if (productToUpdate == null) 
            { 
                return NotFound(); 
            }
            if (await TryUpdateModelAsync<Product>(productToUpdate, "Product", i => i.Name, i => i.Price, i => i.Manufacturer))
            {
                UpdateProductCategories(_context, selectedCategories, productToUpdate); 
                await _context.SaveChangesAsync(); 
                return RedirectToPage("./Index");
            }
            UpdateProductCategories(_context, selectedCategories, productToUpdate); 
            PopulateAssignedCategoryData(_context, productToUpdate); 
            return Page();

        }
        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ID == id);
            

        }


    }
}

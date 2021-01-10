using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proiect.Data;

namespace Proiect.Models
{
    public class ProductCategoriesPageModel:PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;

        //metoda de populare
        public void PopulateAssignedCategoryData(ProiectContext context, Product product)
        {
            var allCategories = context.Category;
            var productCategories = new HashSet<int>(product.ProductCategories.Select(c => c.ProductID));
            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData { CategoryID = cat.ID, Name = cat.CategoryName, Assigned = productCategories.Contains(cat.ID) });
            }
        }

        public void UpdateProductCategories(ProiectContext context, string[] selectedCategories, Product productToUpdate)
        {
            if (selectedCategories == null) { 
                productToUpdate.ProductCategories = new List<ProductCategory>();
                return; 
            }
            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var productCategories = new HashSet<int>(productToUpdate.ProductCategories.Select(c => c.Category.ID));
            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!productCategories.Contains(cat.ID))
                    {
                        productToUpdate.ProductCategories.Add(new ProductCategory { ProductID = productToUpdate.ID, CategoryID = cat.ID });
                    }
                }
                else { 
                    if (productCategories.Contains(cat.ID)) 
                    { 
                        ProductCategory courseToRemove = productToUpdate.ProductCategories.SingleOrDefault(i => i.CategoryID == cat.ID); 
                        context.Remove(courseToRemove); 
                    } 
                }
            }
        }
    }
}

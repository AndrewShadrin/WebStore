using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Domain;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData productData;

        public CatalogController(IProductData productData)
        {
            this.productData = productData;
        }
        public IActionResult Shop(int? sectionId, int? brandId)
        {
            var products = productData.GetProducts(new ProductFilter { SectionId = sectionId, BrandId = brandId }).FromDTO();
            return View(new CatalogViewModel 
            {
                BrandId = brandId,
                SectionId = sectionId,
                Products = products.ToView().OrderBy(p => p.Order)
            });
        }
   
        public IActionResult Details(int id)
        {
            var product = productData.GetProductById(id).FromDTO();

            if (product is null)
            {
                return NotFound();
            }

            return View(product.ToView());
        }
    }
}

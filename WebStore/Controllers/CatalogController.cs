using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Mapping;
using WebStore.ViewModels;

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
            var products = productData.GetProducts(new ProductFilter { SectionId = sectionId, BrandId = brandId });
            return View(new CatalogViewModel 
            {
                BrandId = brandId,
                SectionId = sectionId,
                Products = products.ToView().OrderBy(p => p.Order)
            });
        }
    }
}

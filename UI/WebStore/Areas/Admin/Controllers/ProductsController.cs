using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Role.Administrator)]
    public class ProductsController : Controller
    {
        private readonly IProductData productData;

        public ProductsController(IProductData productData)
        {
            this.productData = productData;
        }

        public IActionResult Index() => View(productData.GetProducts().FromDTO());

        public IActionResult Edit(int id)
        {
            var product = productData.GetProductById(id);

            if (product is null)
            {
                return NotFound();
            }
            return View(product.FromDTO());
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var product = productData.GetProductById(id);

            if (product is null)
            {
                return NotFound();
            }
            return View(product.FromDTO());
        }

        [HttpPost]
        public IActionResult DeleteConfirm(Product product)
        {
            
            return RedirectToAction(nameof(Index));
        }

    }
}

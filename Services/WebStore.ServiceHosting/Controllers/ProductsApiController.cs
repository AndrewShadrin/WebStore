using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebApi.Products)]
    [ApiController]
    public class ProductsApiController : ControllerBase, IProductData
    {
        private readonly IProductData productData;

        public ProductsApiController(IProductData productData) => this.productData = productData;

        [HttpGet("brands")]
        public IEnumerable<BrandDTO> GetBrands()
        {
            return productData.GetBrands();
        }

        [HttpGet("{id}")]
        public ProductDTO GetProductById(int id)
        {
            return productData.GetProductById(id);
        }

        [HttpPost]
        public IEnumerable<ProductDTO> GetProducts([FromBody] ProductFilter productFilter = null)
        {
            return productData.GetProducts(productFilter);
        }

        [HttpGet("sections")]
        public IEnumerable<SectionDTO> GetSections()
        {
            return productData.GetSections();
        }
    }
}

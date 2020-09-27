using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(IConfiguration configuration) : base(configuration, WebApi.Products){}

        public IEnumerable<BrandDTO> GetBrands() => Get<IEnumerable<BrandDTO>>($"{serviceAddress}/brands");

        public ProductDTO GetProductById(int id) => Get<ProductDTO>($"{serviceAddress}/{id}");

        public IEnumerable<ProductDTO> GetProducts(ProductFilter productFilter = null)
        {
            return Post(serviceAddress, productFilter ?? new ProductFilter())
                    .Content
                    .ReadAsAsync<IEnumerable<ProductDTO>>()
                    .Result;
        }

        public IEnumerable<SectionDTO> GetSections() => Get<IEnumerable<SectionDTO>>($"{serviceAddress}/sections");
    }
}

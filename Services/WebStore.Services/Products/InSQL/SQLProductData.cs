using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Services.Products.InSQL
{
    public class SQLProductData : IProductData
    {
        private readonly WebStoreDB dB;

        public SQLProductData(WebStoreDB dB)
        {
            this.dB = dB;
        }

        public IEnumerable<BrandDTO> GetBrands()
        {
            return dB.Brands.ToDTO();
        }

        public IEnumerable<SectionDTO> GetSections()
        {
            return dB.Sections.ToDTO();
        }

        public IEnumerable<ProductDTO> GetProducts(ProductFilter filter = null)
        {
            IQueryable<Product> query = dB.Products
                .Include(product => product.Brand)
                .Include(product => product.Section);

            if (filter?.Ids?.Length > 0)
            {
                query = query.Where(product => filter.Ids.Contains(product.Id));
            }
            else
            {
                if (filter?.BrandId != null)
                {
                    query = query.Where(product => product.BrandId == filter.BrandId);
                }

                if (filter?.SectionId != null)
                {
                    query = query.Where(product => product.SectionId == filter.SectionId);
                }
            }

            return query.AsEnumerable().ToDTO();/*ToArray*/
        }

        public ProductDTO GetProductById(int id)
        {
            return dB.Products
                .Include(product => product.Brand)
                .Include(product => product.Section)
                .FirstOrDefault(product => product.Id == id)
                .ToDTO();
        }
    }
}

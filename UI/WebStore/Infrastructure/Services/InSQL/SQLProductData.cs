using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services.InSQL
{
    public class SQLProductData : IProductData
    {
        private readonly WebStoreDB dB;

        public SQLProductData(WebStoreDB dB)
        {
            this.dB = dB;
        }

        public IEnumerable<Brand> GetBrands()
        {
            return dB.Brands;
        }

        public IEnumerable<Section> GetSections()
        {
            return dB.Sections;
        }

        public IEnumerable<Product> GetProducts(ProductFilter filter = null)
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

            return query;/*ToArray*/
        }

        public Product GetProductById(int id)
        {
            return dB.Products
                .Include(product => product.Brand)
                .Include(product => product.Section)
                .FirstOrDefault(product => product.Id == id);
        }
    }
}

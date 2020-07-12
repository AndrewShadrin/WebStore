using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;

namespace WebStore.Data
{
    public class WebStoreDBInitializer
    {
        private readonly WebStoreDB dB;

        public WebStoreDBInitializer(WebStoreDB dB)
        {
            this.dB = dB;
        }

        public void Initialize()
        {
            var db = dB.Database;

            //if (db.EnsureDeleted())
            //{
            //    if (!db.EnsureCreated())
            //    {
            //        throw new InvalidOperationException("Ошибка при создзании БД");
            //    }
            //}

            db.Migrate();

            if (dB.Products.Any())
            {
                return;
            }

            using (db.BeginTransaction())
            {
                dB.Sections.AddRange(TestData.Sections);
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ProductSections] ON");
                dB.SaveChanges();
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ProductSections] OFF");
                db.CommitTransaction();
            }

            using (db.BeginTransaction())
            {
                dB.Brands.AddRange(TestData.Brands);
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ProductBrands] ON");
                dB.SaveChanges();
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ProductBrands] OFF");
                db.CommitTransaction();
            }

            using (db.BeginTransaction())
            {
                dB.Products.AddRange(TestData.Products);
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] ON");
                dB.SaveChanges();
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] OFF");
                db.CommitTransaction();
            }

            //var products = TestData.Products;
            //var sections = TestData.Sections;
            //var brands = TestData.Brands;

            //var products_section = products.Join(
            //    sections, 
            //    p => p.SectionId, 
            //    s => s.Id, 
            //    (product, section) => (product, section));

            //foreach (var (product, section) in products_section)
            //{
            //    product.Section = section;
            //    product.SectionId = 0;
            //}

            //var products_brand = products.Join(
            //    brands, 
            //    p => p.SectionId, 
            //    b => b.Id, 
            //    (product, brand) => (product, brand));

            //foreach (var (product, brand) in products_brand)
            //{
            //    product.Brand = brand;
            //    product.BrandId= null;
            //}

            //foreach (var product in products)
            //{
            //    product.Id = 0;
            //}

            //var child_sections = sections.Join(
            //    sections,
            //    child => child.ParentId,
            //    parent => parent.Id,
            //    (child, parent) => (child, parent));

            //foreach (var (child, parent) in child_sections)
            //{
            //    child.ParentSection = parent;
            //    child.ParentId = null;
            //}

            //foreach (var section in sections)
            //{
            //    section.Id = 0;
            //}

            //foreach (var brand in brands)
            //{
            //    brand.Id = 0;
            //}

            //using (db.BeginTransaction())
            //{
            //    dB.Sections.AddRange(sections);
            //    dB.Brands.AddRange(brands);
            //    dB.Products.AddRange(products);
            //    dB.SaveChanges();
            //    db.CommitTransaction();
            //}
        }
    }
}

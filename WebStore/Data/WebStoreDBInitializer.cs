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

        }
    }
}

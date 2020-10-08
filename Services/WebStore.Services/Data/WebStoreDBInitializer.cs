using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Services.Data
{
    public class WebStoreDBInitializer
    {
        private readonly WebStoreDB dB;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly ILogger<WebStoreDBInitializer> logger;

        public WebStoreDBInitializer(WebStoreDB dB, UserManager<User> userManager, RoleManager<Role> roleManager, ILogger<WebStoreDBInitializer> logger)
        {
            this.dB = dB;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
        }

        public void Initialize()
        {
            logger.LogInformation("Инициализация БД...");

            var db = dB.Database;

            //if (db.EnsureDeleted())
            //{
            //    if (!db.EnsureCreated())
            //    {
            //        throw new InvalidOperationException("Ошибка при создзании БД");
            //    }
            //}

            try
            {
                logger.LogInformation("Проведение миграций БД");
                db.Migrate();

                logger.LogInformation("Инициализация каталога товаров");
                InitializeProducts();

                logger.LogInformation("Инициализация каталога сотрудников");
                InitializeEmployees();

                logger.LogInformation("Инициализация данных системы Identity");
                InitializeIdentityAsync().Wait();
            }
            catch (Exception error)
            {
                logger.LogCritical(new EventId(0), error, "Ошибка инициализации БД");

                throw;
            }
            
            logger.LogInformation("Инициализация БД выполнена успешно");
        }

        private async Task InitializeIdentityAsync()
        {
            async Task CheckRoleExist(string roleName)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    logger.LogInformation("Добавление роли пользователей {0}", roleName);

                    await roleManager.CreateAsync(new Role { Name = roleName });
                }
            }

            await CheckRoleExist(Role.Administrator);
            await CheckRoleExist(Role.User);

            if (await userManager.FindByNameAsync(User.Administrator) is null)
            {
                var admin = new User { UserName = User.Administrator };
                var creationResult = await userManager.CreateAsync(admin, User.DefaultAdminPassword);
                if (creationResult.Succeeded)
                {
                    logger.LogInformation("Пользователь {0} добавлен", User.Administrator);

                    await userManager.AddToRoleAsync(admin, Role.Administrator);

                    logger.LogInformation("Пользователю {0} добавлена роль {1}", User.Administrator, Role.Administrator);
                }
                else
                {
                    var errors = creationResult.Errors.Select(e => e.Description);
                    throw new InvalidOperationException($"Ошибка при создании пользователя Администратор: {string.Join(", ", errors)}");
                }
            }
        }

        private void InitializeProducts()
        {
            var db = dB.Database;

            if (dB.Products.Any())
            {
                logger.LogInformation("Каталог товаров уже инициализирован");

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

        private void InitializeEmployees()
        {
            if (dB.Employees.Any())
            {
                logger.LogInformation("Раздел соторудников уже инициализирован");

                return;
            }

            using (dB.Database.BeginTransaction())
            {
                TestData.Employees.ForEach(employee => employee.Id = 0);

                dB.Employees.AddRange(TestData.Employees);
                dB.SaveChanges();
                dB.Database.CommitTransaction();
            }
        }
    }
}

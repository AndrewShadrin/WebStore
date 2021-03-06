﻿using System.Collections.Generic;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;

namespace WebStore.Interfaces.Services
{
    public interface IProductData
    {
        IEnumerable<SectionDTO> GetSections();

        IEnumerable<BrandDTO> GetBrands();

        IEnumerable<ProductDTO> GetProducts(ProductFilter productFilter = null);

        ProductDTO GetProductById(int id);
    }
}

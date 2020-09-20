using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Components
{
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductData productData;

        public SectionsViewComponent(IProductData productData)
        {
            this.productData = productData;
        }
        public IViewComponentResult Invoke()
        {
            var sections = GetSections();
            return View(sections);
        }

        private IEnumerable<SectionViewModel> GetSections()
        {
            var sections = productData.GetSections().ToArray();

            var parentSections = sections.Where(s => s.ParentId is null);

            var parentSectionsViews = parentSections
                .Select(s => new SectionViewModel
                { 
                    Id = s.Id,
                    Name = s.Name,
                    Order = s.Order
                })
                .ToList();

            foreach (var parentSection in parentSectionsViews)
            {
                var childs = sections.Where(s => s.ParentId == parentSection.Id);

                foreach (var childSection in childs)
                {
                    parentSection.ChildSections.Add(new SectionViewModel
                    {
                        Id = childSection.Id,
                        Name = childSection.Name,
                        Order = childSection.Order,
                        ParentSection = parentSection
                    });
                }
                parentSection.ChildSections.Sort((a, b) => Comparer<double>.Default.Compare(a.Order, b.Order));
            }

            parentSectionsViews.Sort((a, b) => Comparer<double>.Default.Compare(a.Order, b.Order));

            return parentSectionsViews;
        }
    }
}

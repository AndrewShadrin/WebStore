using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Mapping;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    //[Route("Users")]
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData employeesData;
        public EmployeesController(IEmployeesData employeesData)
        {
            this.employeesData = employeesData;
        }

        //[Route("User-{id}")]
        public IActionResult Details(int id)
        {
            var employee = employeesData.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee.ToView());

        }

        //[Route("All")]
        public IActionResult Index()
        {
            return View(employeesData.Get().ToView());
        }

        #region Edit

        public IActionResult Edit(int? id)
        {
            if (id is null)
            {
                return View(new EmployeesViewModel());
            }

            if (id < 0)
            {
                return BadRequest();
            }

            var employee = employeesData.GetById((int)id);
            if (employee is null)
            {
                return NotFound();
            }

            return View(employee.ToView());
        }

        [HttpPost]
        public IActionResult Edit(EmployeesViewModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (model.Age < 18 || model.Age > 75)
            {
                ModelState.AddModelError(nameof(model.Age), "Возраст должен быть в пределах от 18 до 75");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Id == 0)
            {
                employeesData.Add(model.FromView());
            }
            else
            {
                employeesData.Edit(model.FromView());
            }
            employeesData.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var employee = employeesData.GetById(id);
            if (employee is null)
            {
                return NotFound();
            }
            
            return View(employee.ToView());

        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            employeesData.Delete(id);
            employeesData.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}

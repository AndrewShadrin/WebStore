using Microsoft.AspNetCore.Mvc;
using System;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    //[Route("Users")]
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
            return View(employee);
        }

        //[Route("All")]
        public IActionResult Index()
        {
            return View(employeesData.Get());
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

            return View(new EmployeesViewModel
            {
                id = employee.Id,
                FirstName = employee.Name,
                LastName = employee.Surname,
                Patronymic = employee.Patronymic,
                Age = employee.Age
            });
        }

        [HttpPost]
        public IActionResult Edit(EmployeesViewModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var employee = new Employee
            {
                Id = model.id,
                Name = model.FirstName,
                Surname = model.LastName,
                Patronymic = model.Patronymic,
                Age = model.Age
            };

            if (model.id == 0)
            {
                employeesData.Add(employee);
            }
            else
            {
                employeesData.Edit(employee);
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
            
            return View(new EmployeesViewModel
            {
                id = employee.Id,
                FirstName = employee.Name,
                LastName = employee.Surname,
                Patronymic = employee.Patronymic,
                Age = employee.Age
            });

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {

        }
    }
}

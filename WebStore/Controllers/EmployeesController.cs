using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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

        static EmployeesViewModel GetEmployeeViewModel(Employee employee)
        {
            return new EmployeesViewModel
            {
                Id = employee.Id,
                FirstName = employee.Name,
                LastName = employee.Surname,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
                EmploymentDate = employee.EmploymentDate
            };
        }

        //[Route("User-{id}")]
        public IActionResult Details(int id)
        {
            var employee = employeesData.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(GetEmployeeViewModel(employee));

        }

        //[Route("All")]
        public IActionResult Index()
        {
            return View(employeesData.Get().Select(employee => GetEmployeeViewModel(employee)));
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

            return View(GetEmployeeViewModel(employee));
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

            var employee = new Employee
            {
                Id = model.Id,
                Name = model.FirstName,
                Surname = model.LastName,
                Patronymic = model.Patronymic,
                Age = model.Age,
                EmploymentDate = model.EmploymentDate
            };

            if (model.Id == 0)
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
            
            return View(GetEmployeeViewModel(employee));

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

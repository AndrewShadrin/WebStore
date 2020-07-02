using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
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
            return RedirectToAction(nameof(Index));
        }
    }
}

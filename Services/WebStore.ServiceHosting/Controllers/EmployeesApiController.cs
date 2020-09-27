using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/employees")]
    [Produces("application/json")]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData employeesData;

        public EmployeesApiController(IEmployeesData employeesData)
        {
            this.employeesData = employeesData;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return employeesData.Get();
        }

        [HttpGet("{id}")]
        public Employee GetById(int id)
        {
            return employeesData.GetById(id);
        }

        [HttpPost]
        public int Add(Employee employee)
        {
            var id = employeesData.Add(employee);
            SaveChanges();
            return id;
        }

        [HttpPut]
        public void Edit(Employee employee)
        {
            employeesData.Edit(employee);
            SaveChanges();
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var result = employeesData.Delete(id);
            SaveChanges();
            return result;
        }

        //[NonAction]
        public void SaveChanges()
        {
            employeesData.SaveChanges();
        }
    }
}
